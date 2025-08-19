<template>
	<n-spin :show="loading">
		<n-data-table
			:columns="columns"
			:data="rooms"
			:pagination="{ pageSize: 10 }"
			:row-key="(row: Room) => row.id"
			class="rooms-table"
		/>
	</n-spin>
</template>

<script setup lang="ts">
import { h } from 'vue'
import { NDataTable, NSpin, NTag, NIcon, NButton, NSpace, type DataTableColumns } from 'naive-ui'
import { CheckCircleOutlined as CheckIcon, CancelOutlined as CancelIcon } from '@vicons/material'
import type { Room } from '@/models/Room'

const props = defineProps<{ rooms: Room[]; loading?: boolean }>()
const emit = defineEmits<{
	(e: 'edit', room: Room): void
	(e: 'toggle', room: Room): void
}>()

const columns: DataTableColumns<Room> = [
	{ title: 'Nombre', key: 'name', sorter: 'default' },
	{ title: 'Capacidad', key: 'capacity', sorter: 'default', render: (row) => `${row.capacity} personas` },
	{ title: 'Tipo', key: 'type', sorter: 'default' },
	{ title: 'Tarifa/Hora', key: 'hourlyRate', sorter: 'default', render: (row) => `€${row.hourlyRate.toFixed(2)}` },
	{
		title: 'Estado',
		key: 'available',
		render(row) {
			return h(
				NTag,
				{ type: row.available ? 'success' : 'error', size: 'small' },
				{
					default: () => (row.available ? 'Disponible' : 'No disponible'),
					icon: row.available
						? () => h(NIcon, null, { default: () => h(CheckIcon) })
						: () => h(NIcon, null, { default: () => h(CancelIcon) })
				}
			)
		}
	},
	{
		title: 'Acciones',
		key: 'actions',
		render(row) {
			return h(NSpace, null, {
				default: () => [
					h(
						NButton,
						{ size: 'small', quaternary: true, type: 'info', onClick: () => emit('edit', row) },
						{ default: () => 'Editar' }
					),
					h(
						NButton,
						{ size: 'small', quaternary: true, type: row.available ? 'error' : 'success', onClick: () => emit('toggle', row) },
						{ default: () => (row.available ? 'Deshabilitar' : 'Habilitar') }
					)
				]
			})
		}
	}
]
</script>

<style scoped>
.rooms-table {
	margin-top: 16px;
}
</style>
