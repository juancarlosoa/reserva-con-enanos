<template>
	<n-spin :show="loading">
		<n-grid :cols="3" :x-gap="16" :y-gap="16" class="providers-grid">
			<n-grid-item v-for="provider in providers" :key="provider.id">
				<ProviderCard
					:provider="provider"
					@click="() => emit('select', provider)"
					@edit="() => emit('edit', provider)"
					@view-rooms="() => emit('view-rooms', provider)"
				/>
			</n-grid-item>
		</n-grid>
	</n-spin>
</template>

<script setup lang="ts">
import { NSpin, NGrid, NGridItem } from 'naive-ui'
import ProviderCard from '@/components/ProviderCard.vue'
import type { Provider } from '@/models/Provider'

defineProps<{ providers: Provider[]; loading?: boolean }>()

const emit = defineEmits<{
	(e: 'select', provider: Provider): void
	(e: 'edit', provider: Provider): void
	(e: 'view-rooms', provider: Provider): void
}>()
</script>

<style scoped>
.providers-grid {
	margin-top: 16px;
}
</style>
