# Enterprise Distributed System

A distributed system built using a Service-Oriented Architecture. It allows a user, known as worker to create tickets in a web page. 
Then, those tickets are answered or sent to other departments by the solver. Finally, each department can answer the tickets assigned to it.

1. [Application Overview](#application-overview)
1. [Technologies](#technologies)
2. [How To Run](#how-to-run)
3. [Screenshots](#screenshots)


## Application Overview

The image below shows the diagram about the system architecture.

![Aplication Architecture](https://user-images.githubusercontent.com/28347544/159259806-84c6e533-ecc3-47ae-b4fa-00e86db1db9e.png)


## Technologies

There were used different technologies for each application. With the help of the image in [application overview](#application-overview) the technologies used are described below. 

### worker (web page)

* React
* React-Bootstrap

### central server (backend) 

* Node.js
* Express
* Mongodb

### solver and department (desktop app) 

* .NET
* GTK#
* RabbitMQ


## How to run

The central server, the worker app and the rabbitMQ application are dockerized. So, if you do not have docker and docker-compose installed you may install it. If you are using ubuntu you may use the following commands

```
sudo apt-get install docker
sudo apt-get install docker-compose
```

To run the forementioned applications: 

1. Go to the root directory 
2. Run: ``` docker-compose up ```
3. You can access the worker app in ```http://localhost:8080 ```

## Screenshots

### worker
![Login Page](https://user-images.githubusercontent.com/28347544/159326843-99815518-ebc4-4f69-b173-85890f42dbf7.png)
![Tickets](https://user-images.githubusercontent.com/28347544/159326898-d29f09a6-5df7-4a10-b24b-8c502e2cc787.png)
![New Ticket](https://user-images.githubusercontent.com/28347544/159327514-ad582fd8-a6fc-4867-9f73-f0ad6b377aa7.png)

### solver

![Issue solving menu](https://user-images.githubusercontent.com/28347544/159329392-2b9056ac-e9fc-4be8-b8ec-502eb37c14d2.png)
![Issues main status](https://user-images.githubusercontent.com/28347544/159330220-2e3597e3-79a1-448d-8b22-1a6911758905.png)

### department
![Department main view](https://user-images.githubusercontent.com/28347544/159330369-97ffbc6c-8576-454a-b7bd-850102b790fa.png)
![Resolve issue](https://user-images.githubusercontent.com/28347544/159334479-8d2deeae-b8e0-46de-88e3-1775bcc34f5b.png)


