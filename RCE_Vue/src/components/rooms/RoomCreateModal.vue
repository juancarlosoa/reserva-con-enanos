<template>
	<n-modal :show="show" @update:show="(v) => emit('update:show', v)">
		<n-card style="width: 600px" title="Crear Nueva Sala" :bordered="false" size="huge" role="dialog" aria-modal="true">
			<n-form ref="formRef" :model="model" :rules="rules">
				<n-form-item path="name" label="Nombre de la Sala">
					<n-input v-model:value="model.name" placeholder="Ej: Sala Principal" />
				</n-form-item>
				<n-form-item path="capacity" label="Capacidad">
					<n-input-number v-model:value="model.capacity" :min="1" :max="100" />
				</n-form-item>
				<n-form-item path="type" label="Tipo de Sala">
					<n-select v-model:value="model.type" :options="roomTypeOptions" />
				</n-form-item>
				<n-form-item path="hourlyRate" label="Tarifa por Hora (€)">
					<n-input-number v-model:value="model.hourlyRate" :min="0" :step="0.01" />
				</n-form-item>
			</n-form>
			<template #footer>
				<n-space justify="end">
					<n-button @click="emitClose()">Cancelar</n-button>
					<n-button type="primary" @click="submit">Crear</n-button>
				</n-space>
			</template>
		</n-card>
	</n-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { NModal, NCard, NForm, NFormItem, NInput, NInputNumber, NSelect, NButton, NSpace, type FormInst } from 'naive-ui'

const { show } = defineProps<{ show: boolean }>()
const emit = defineEmits<{
	(e: 'update:show', value: boolean): void
	(e: 'submit', payload: { name: string; capacity: number; type: string; hourlyRate: number }): void
}>()

const formRef = ref<FormInst | null>(null)
const model = ref({ name: '', capacity: 4, type: '', hourlyRate: 25.0 })

const rules = {
	name: { required: true, message: 'Por favor ingrese el nombre de la sala', trigger: ['input', 'blur'] },
	type: { required: true, message: 'Por favor seleccione el tipo de sala', trigger: ['change', 'blur'] }
}

const roomTypeOptions = [
	{ label: 'Terror', value: 'Terror' },
	{ label: 'Aventura', value: 'Aventura' },
	{ label: 'Misterio', value: 'Misterio' },
	{ label: 'Ciencia Ficción', value: 'Ciencia Ficción' },
	{ label: 'Fantasía', value: 'Fantasía' }
]

watch(
	() => show,
	(visible) => {
		if (visible) {
			model.value = { name: '', capacity: 4, type: '', hourlyRate: 25.0 }
			formRef.value?.restoreValidation()
		} else {
			model.value = { name: '', capacity: 4, type: '', hourlyRate: 25.0 }
		}
	}
)

function emitClose() {
	emit('update:show', false)
}

function submit() {
	formRef.value?.validate((errors) => {
		if (!errors) {
			emit('submit', { ...model.value })
			model.value = { name: '', capacity: 4, type: '', hourlyRate: 25.0 }
			emitClose()
		}
	})
}
</script>
