<template>
  <n-card hoverable class="provider-card" @click="emit('click')">
    <template #header>
      <div class="card-header">
        <n-avatar :size="40" :src="provider.logo">
          {{ provider.name.charAt(0) }}
        </n-avatar>
        <div class="provider-info">
          <h3>{{ provider.name }}</h3>
          <n-tag :type="provider.active ? 'success' : 'warning'" size="small">
            {{ provider.active ? 'Activo' : 'Inactivo' }}
          </n-tag>
        </div>
      </div>
    </template>
    <div class="provider-stats">
      <n-statistic label="Salas totales" :value="provider.roomCount" />
      <n-statistic label="Reservas este mes" :value="provider.bookingsThisMonth" />
    </div>
    <template #footer>
      <n-space justify="space-between">
        <n-button quaternary size="small" @click.stop="emit('edit')">
          Editar
        </n-button>
        <n-button quaternary size="small" @click.stop="emit('view-rooms')">
          Ver Salas
        </n-button>
      </n-space>
    </template>
  </n-card>
  
</template>

<script setup lang="ts">
import { NCard, NAvatar, NTag, NStatistic, NSpace, NButton } from 'naive-ui'
import type { Provider } from '@/models/Provider'

const props = defineProps<{ provider: Provider }>()
const emit = defineEmits<{
  (e: 'click'): void
  (e: 'edit'): void
  (e: 'view-rooms'): void
}>()
</script>

<style scoped>
.provider-card {
  cursor: pointer;
  transition: transform 0.2s ease;
}

.provider-card:hover {
  transform: translateY(-2px);
}

.card-header {
  display: flex;
  align-items: center;
  gap: 12px;
}

.provider-info h3 {
  margin: 0 0 4px 0;
  font-size: 16px;
  font-weight: 600;
}

.provider-stats {
  display: flex;
  justify-content: space-between;
  margin: 16px 0;
}
</style>

