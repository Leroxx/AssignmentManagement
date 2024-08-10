# Assignment API

- [Assignment API](#assignment-api)
  - [Domain Overview](#domain-overview)
  - [Run the service using the .NET CLI](#run-the-service-using-the-net-cli)
- [Api Definition](#api-definition)
  - [Create Assignment](#create-assignment)
    - [Create Assignment Request](#create-assignment-request)
    - [Create Assignment Response](#create-assignment-response)
  - [Get Assignment](#get-assignment)
    - [Get Assignment Request](#get-assignment-request)
    - [Get Assignment Response](#get-assignment-response)
  - [Update Assignment](#update-assignment)
    - [Update Assignment Request](#update-assignment-request)
    - [Update Assignment Response](#update-assignment-response)
  - [Complete Assignment](#complete-assignment)
    - [Complete Assignment Request](#complete-assignment-request)
    - [Complete Assignment Response](#complete-assignment-response)
  - [Delete Assignment](#delete-assignment)
    - [Delete Assignment Request](#delete-assignment-request)
    - [Delete Assignment Response](#delete-assignment-response)

# Domain Overview

This is a simple assignment application. 
It allows users to create and manage their assignment.

It use sqlite for data storage.

## Run the service using the .NET CLI

```shell
dotnet build
```

```shell
dotnet run --project src/AssignmentManagement.Api
```

# Api Definition

## Create Assignment

### Create Assignment Request

```js
POST /assignments
```

```json
{
    "name": "Complete Assignment",
    "description": "Complete all assingments of the day"
}
```

### Create Assignment Response

```js
201 Created
```

```yml
Location: {{host}}/Assignments/{{id}}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "Complete Assignment",
    "description": "Complete all assingments of the day",
    "state" : "InProgress",
    "createDateTime": "2022-04-08T08:00:00",
    "lastModifiedDateTime": "2022-04-06T12:00:00"
}
```

## Get Assignment

### Get Assignment Request

```js
GET /assignments/{{id}}
```

### Get Assignment Response

```js
200 Ok
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "Complete Assignment",
    "description": "Complete all assingments of the day",
    "state" : "InProgress", /*"Completed"*/
    "createDateTime": "2022-04-08T08:00:00",
    "lastModifiedDateTime": "2022-04-06T12:00:00"
}
```

## Update Assignment

### Update Assignment Request

```js
PUT /assignments/{{id}}
```

```json
{
    "name": "Complete Assignment",
    "description": "Complete all assingments of the day"
}
```

### Update Assignment Response

```js
204 No Content
```

```yml
Location: {{host}}/Assignments/{{id}}
```

## Complete Assignment

### Complete Assignment Request

```js
PUT /assignments/{{id}}
```

### Complete Assignment Response

```js
204 No Content
```

```yml
Location: {{host}}/assignments/complete{{id}}
```

## Delete Assignment

### Delete Assignment Request

```js
DELETE /assignments/{{id}}
```

### Delete Assignment Response

```js
204 No Content
```
