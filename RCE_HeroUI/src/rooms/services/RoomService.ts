import { RoomRequestDTO } from "../dtos/RoomRequestDTO";
import { Room } from "../models/Room";
import { roomRepository } from "../repositories/RoomRepository";

export const RoomService = {
    
    getProviderById: async (roomId: string): Promise<Room> => {
        const dto = await roomRepository.getRoomById(roomId);
 
        return new Room(dto);
    },
 
    createRoom: async (data: RoomRequestDTO): Promise<Room> => {
        const dto = await roomRepository.createRoom(data);
 
        return new Room(dto);
    },
 
    deleteRoom: async  (roomId: string): Promise<Room> => {
        const dto = await roomRepository.deleteRoom(roomId);
 
        return new Room(dto);
     },
 
     updateRoom:  async (roomId: string, data: RoomRequestDTO): Promise<Room> => {
         const dto = await roomRepository.updateProvider(roomId, data);
 
         return new Room(dto);
     },   
}