<template>
	<n-modal :show="show" @update:show="(v) => emit('update:show', v)">
		<n-card style="width: 600px" title="Crear Nuevo Proveedor" :bordered="false" size="huge" role="dialog" aria-modal="true">
			<n-form ref="formRef" :model="model" :rules="rules">
				<n-form-item path="name" label="Nombre">
					<n-input v-model:value="model.name" placeholder="Nombre del proveedor" />
				</n-form-item>
				<n-form-item path="email" label="Email">
					<n-input v-model:value="model.email" placeholder="email@ejemplo.com" />
				</n-form-item>
				<n-form-item path="phone" label="Teléfono">
					<n-input v-model:value="model.phone" placeholder="+34 123 456 789" />
				</n-form-item>
				<n-form-item path="address" label="Dirección">
					<n-input v-model:value="model.address" placeholder="Dirección completa" />
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
import { NModal, NCard, NForm, NFormItem, NInput, NButton, NSpace, type FormInst } from 'naive-ui'

const { show } = defineProps<{ show: boolean }>()
const emit = defineEmits<{
	(e: 'update:show', value: boolean): void
	(e: 'submit', payload: { name: string; email: string; phone: string; address: string }): void
}>()

const formRef = ref<FormInst | null>(null)
const model = ref({ name: '', email: '', phone: '', address: '' })

const rules = {
	name: { required: true, message: 'Por favor ingrese el nombre del proveedor', trigger: ['input', 'blur'] },
	email: { required: true, message: 'Por favor ingrese un email válido', trigger: ['input', 'blur'] }
}

watch(
	() => show,
	(visible) => {
		if (visible) {
			model.value = { name: '', email: '', phone: '', address: '' }
			formRef.value?.restoreValidation()
		} else {
			// opcional: limpiar al cerrar también
			model.value = { name: '', email: '', phone: '', address: '' }
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
			model.value = { name: '', email: '', phone: '', address: '' }
			emitClose()
		}
	})
}
</script>
