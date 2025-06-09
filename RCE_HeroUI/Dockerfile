FROM node:18-alpine

WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build

RUN npm install -g serve

EXPOSE 8080

# Comando final: servir la app con 'serve'
CMD ["serve", "-s", "dist", "-l", "8080"]