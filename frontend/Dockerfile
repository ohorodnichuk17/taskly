FROM node:18 AS build
WORKDIR /app

COPY frontend/package*.json ./frontend/
RUN cd frontend && npm install

COPY frontend ./frontend

RUN cd frontend && npm run build

# Production image
FROM node:18 AS production
WORKDIR /app

RUN npm install -g serve

COPY --from=build /app/frontend/dist .

EXPOSE 3000
CMD ["serve", "-s", ".", "-l", "3000"]
