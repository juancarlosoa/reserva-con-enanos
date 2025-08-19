<template>
  <div class="dashboard-container">
    <!-- Header -->
    <n-layout-header bordered class="header">
      <div class="header-content">
        <h1 class="title">
          <n-icon size="24" class="title-icon">
            <HomeIcon />
          </n-icon>
          Panel de Control - Reserva con Enanos
        </h1>
        <n-breadcrumb>
          <n-breadcrumb-item @click="goToProviders">Proveedores</n-breadcrumb-item>
          <n-breadcrumb-item v-if="selectedProvider">{{ selectedProvider.name }}</n-breadcrumb-item>
          <n-breadcrumb-item v-if="currentView === 'rooms'">Salas</n-breadcrumb-item>
        </n-breadcrumb>
      </div>
    </n-layout-header>

    <!-- Main Content -->
    <n-layout class="main-layout" has-sider>
      <!-- Sidebar -->
      <n-layout-sider
        bordered
        collapse-mode="width"
        :collapsed-width="64"
        :width="240"
        :collapsed="collapsed"
        show-trigger
        @collapse="collapsed = true"
        @expand="collapsed = false"
        class="sidebar"
      >
        <n-menu
          :collapsed="collapsed"
          :collapsed-width="64"
          :collapsed-icon-size="22"
          :options="menuOptions"
          :value="selectedMenuKey"
          @update:value="handleMenuSelect"
        />
      </n-layout-sider>

      <!-- Content Area -->
      <n-layout-content class="content">
        <!-- Providers View -->
        <div v-if="currentView === 'providers'" class="view-container">
          <div class="view-header">
            <h2>Gestión de Proveedores</h2>
            <n-button type="primary" @click="showCreateProviderModal = true">
              <template #icon>
                <n-icon><PlusIcon /></n-icon>
              </template>
              Nuevo Proveedor
            </n-button>
          </div>

          <ProviderList
            :providers="providers"
            :loading="providersLoading"
            @select="selectProvider"
            @edit="editProvider"
            @view-rooms="viewRooms"
          />
        </div>

        <!-- Rooms View -->
        <div v-if="currentView === 'rooms'" class="view-container">
          <div class="view-header">
            <div>
              <h2>Salas de {{ selectedProvider?.name }}</h2>
              <n-button text @click="goToProviders" class="back-button">
                <template #icon><n-icon><ArrowBackIcon /></n-icon></template>
                Volver a Proveedores
              </n-button>
            </div>
            <n-button type="primary" @click="showCreateRoomModal = true">
              <template #icon>
                <n-icon><PlusIcon /></n-icon>
              </template>
              Nueva Sala
            </n-button>
          </div>

          <RoomTable
            :rooms="currentRooms"
            :loading="roomsLoading"
            @edit="editRoom"
            @toggle="toggleRoomAvailability"
          />
        </div>
      </n-layout-content>
    </n-layout>

    <!-- Modales -->
    <ProviderCreateModal
      v-model:show="showCreateProviderModal"
      @submit="handleCreateProvider"
    />
    <RoomCreateModal
      v-model:show="showCreateRoomModal"
      @submit="handleCreateRoom"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, h, onMounted } from 'vue'
import {
  NLayout,
  NLayoutHeader,
  NLayoutSider,
  NLayoutContent,
  NMenu,
  NButton,
  NIcon,
  NBreadcrumb,
  NBreadcrumbItem,
  useMessage
} from 'naive-ui'
import {
  HomeOutlined as HomeIcon,
  AddOutlined as PlusIcon,
  ArrowBackOutlined as ArrowBackIcon
} from '@vicons/material'
import { Provider } from '@/models/Provider'
import { Room } from '@/models/Room'
import { providerRepository } from '@/repositories'

// Componentes reutilizables
import ProviderList from '@/components/providers/ProviderList.vue'
import ProviderCreateModal from '@/components/providers/ProviderCreateModal.vue'
import RoomTable from '@/components/rooms/RoomTable.vue'
import RoomCreateModal from '@/components/rooms/RoomCreateModal.vue'
import { roomRepository } from '@/repositories/room/room.repository'

// Reactive data
const message = useMessage()
const collapsed = ref(false)
const currentView = ref<'providers' | 'rooms'>('providers')
const selectedProvider = ref<Provider | null>(null)
const selectedMenuKey = ref('providers')
const showCreateProviderModal = ref(false)
const showCreateRoomModal = ref(false)

// Data
const providers = ref<Provider[]>([])
const rooms = ref<Room[]>([])
const providersLoading = ref(false)
const roomsLoading = ref(false)

// Computed
const currentRooms = computed(() => {
  if (!selectedProvider.value) return []
  return rooms.value.filter(room => room.providerId === selectedProvider.value?.id)
})

// Menu options
const menuOptions = computed(() => [
  {
    label: 'Proveedores',
    key: 'providers',
    icon: () => h(NIcon, null, { default: () => h(HomeIcon) })
  }
])

// Methods
const handleMenuSelect = (key: string) => {
  selectedMenuKey.value = key
  if (key === 'providers') {
    goToProviders()
  }
}

const selectProvider = async (provider: Provider) => {
  selectedProvider.value = provider
  await viewRooms(provider)
}

const viewRooms = async (provider: Provider) => {
  selectedProvider.value = provider
  currentView.value = 'rooms'
  try {
    roomsLoading.value = true
    rooms.value = await providerRepository.getProviderRooms(provider.id)
  } catch (e: any) {
    message.error(e?.message || 'Error cargando salas')
  } finally {
    roomsLoading.value = false
  }
}

const goToProviders = () => {
  currentView.value = 'providers'
  selectedProvider.value = null
  selectedMenuKey.value = 'providers'
}

const editProvider = (provider: Provider) => {
  message.info(`Editar proveedor: ${provider.name}`)
}

const editRoom = (room: Room) => {
  message.info(`Editar sala: ${room.name}`)
}

const toggleRoomAvailability = (room: Room) => {
  room.available = !room.available
  message.success(`Sala ${room.name} ${room.available ? 'habilitada' : 'deshabilitada'}`)
}

async function handleCreateProvider(payload: { name: string; email: string; phone: string; address: string }) {
  try {
    const providerToCreate: Provider = {
      id: "0",
      name: payload.name,
      email: payload.email,
      phone: payload.phone,
      address: payload.address,
      active: true,
      roomCount: 0,
      bookingsThisMonth: 0
      }
    const created = await providerRepository.createProvider(providerToCreate)
    providers.value.push(created)
      message.success('Proveedor creado exitosamente')
  } catch (e: any) {
    message.error(e?.message || 'No se pudo crear el proveedor')
    }
}

async function handleCreateRoom(payload: { name: string; capacity: number; type: string; hourlyRate: number }) {
  if (!selectedProvider.value) return
      const room: Room = {
        id: "0",
    name: payload.name,
    capacity: payload.capacity,
    type: payload.type,
    hourlyRate: payload.hourlyRate,
        available: true,
    providerId: selectedProvider.value.id
      }
      const provider = providers.value.find(p => p.id === selectedProvider.value!.id)
      if (provider) {
        provider.roomCount++
      }
      const created = await roomRepository.createRoom(room)
      rooms.value.push(created)
      message.success('Sala creada exitosamente')
    }

async function loadProviders() {
  try {
    providersLoading.value = true
    providers.value = await providerRepository.getProviders()
  } catch (e: any) {
    message.error(e?.message || 'Error cargando proveedores')
  } finally {
    providersLoading.value = false
  }
}

onMounted(loadProviders)
</script>

<style scoped>
.dashboard-container {
  height: 100vh;
  display: flex;
  flex-direction: column;
}

.header {
  padding: 0 24px;
  height: 64px;
}

.header-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
  height: 100%;
}

.title {
  display: flex;
  align-items: center;
  gap: 8px;
  margin: 0;
  font-size: 18px;
  font-weight: 600;
}

.title-icon {
  color: #18a058;
}

.main-layout {
  flex: 1;
  height: calc(100vh - 64px);
}

.sidebar {
  height: 100%;
}

.content {
  padding: 24px;
  overflow-y: auto;
}

.view-container {
  max-width: 1200px;
}

.view-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 24px;
}

.view-header h2 {
  margin: 0;
  font-size: 24px;
  font-weight: 600;
}

.back-button {
  margin-top: 4px;
}

.rooms-table {
  margin-top: 16px;
}

@media (max-width: 768px) {
  .content {
    padding: 16px;
  }
  
  .view-header {
    flex-direction: column;
    gap: 16px;
    align-items: stretch;
  }
}
</style>