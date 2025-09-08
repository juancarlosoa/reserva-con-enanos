export interface Room {
  id: string
  slug: string
  name: string
  capacity: number
  type: string
  hourlyRate: number
  available: boolean
  providerSlug: string
}