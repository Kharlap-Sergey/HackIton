FROM node:18-alpine

WORKDIR /app/client

COPY src/Client/package.json .

RUN npm install

COPY src/Client .

RUN npm run build

EXPOSE 8080

CMD [ "npm", "run", "preview" ]