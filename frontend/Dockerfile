FROM node:18 AS build
WORKDIR app/frontend

COPY package*.json ./
RUN npm install

COPY . .

RUN npm run build

FROM node:18 AS production
WORKDIR app/frontend

RUN npm install -g serve

COPY --from=build app/frontend/dist .

EXPOSE 3000
CMD ["serve", "-s", ".", "-l", "3000"]
