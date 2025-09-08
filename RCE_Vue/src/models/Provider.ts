export interface Provider {
  id: string
  slug: string
  name: string
  email: string
  phone: string
  address: string
  logo?: string
  active: boolean
  roomCount: number
  bookingsThisMonth: number
}