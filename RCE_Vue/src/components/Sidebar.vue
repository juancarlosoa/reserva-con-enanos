<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const active = ref('/dashboard')
const menuOptions = [
  { label: 'Dashboard', key: '/dashboard' },
  { label: 'Users', key: '/users' },
  { label: 'Settings', key: '/settings' },
]

function navigate(key: string) {
  router.push(key)
}
</script>

<template>
  <n-layout-sider width="240" bordered>
    <n-menu
      v-model:value="active"
      :options="menuOptions"
      @update:value="navigate"
    />
  </n-layout-sider>
</template>