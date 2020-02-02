
# VehicleTrackingAPI - (REST API)

This is a very basic ASP.NET Core 2.2 REST API project. 
The goal is to build a REST API to register vehicles and track Geo position of the registered vehicles. 

## Prerequisite

1) .NET Core 2.2 framework
2) MongoDB server
- Execute the following command to run the MongoDB server as a service. Note that here I have used my C: drive path:
>mongod --dbpath="C:\Program Files\MongoDB\Server\4.2\bin" --logpath="C:\Program Files\MongoDB\Log\log.txt" --install

- Start the MongoDB service from Services manager. 

## Knowledge Required to Setup & Run Project
1. Set the valid Google API Key in the following appsettings section:
     -- **GoogleMapApiConfig.ApiKey**: To execute the location name by the device position.

2.  Set the following JwtConfiguration when applicable:
     -- **JwtConfig.Issuer**:  Correct issuer url which is the VehicleTrackingAPI url in the local machine.
     -- **JwtConfig.ExpireTimeInMins:** To increase the token expire time. Default is 60 minutes.  
     
3. Set the following VehicleTrackerDbConfig when applicable:
    -- **VehicleTrackerDbConfig.ConnectionString:** Correct MongoDB connection url which is the MongoDB server suppose to use.
    
4. To Test the following API:
     -- /api/Tracking/CurrentTracking?registrationId={0}
     -- /api/Tracking/TrackingsInCertainTime?registrationId={0}&startTime={1}&endTime={2}
     
   Token API: *api/Token?userName={0}&password={1}* need to be called first with admin credential given in appsettings which is: **admin/admin@123**
   With the received token, above two API need to be called, otherwise 401 status code will be received.  

## Library Used
Used following library in the project:
- MediatR (Used to compose message, create and listen events)
- MongoDB.Driver (Used to communicate with MongoDB)
- Serilog (Used to log)
- Swashbuckle (Swagger, used to expose API Documentation Endpoint)

## API Details
VehicleTrackingAPI is implemented using CQRS design pattern. 
MediatR library is used to handle the communication between:
- Command & Command Handler
- Query & Query Handler 

### POST API  
For the POST API, there are 2 Commands.
Followings are the API and Command summary:

(1) **/api/Registration** 
Command used: **AddRegistrationCommand**
Responsibility: Register a new device. 
In each vehicle there is a device which sends it's position. Assuming this device id is unique per vehicle, by which vehicle registration is performed. 
     
(2) **/api/Tracking** 
Command used: **AddTrackingCommand**
Responsibility: Track the device location for the registered device. 
RegistrationId is used to track the device location which ensures a device or user cannot add/update the position of another vehicle.

### GET API
For the GET API, there are 5 Queries.
Followings are the API and Query summary:

(1) **/api/Token?userName={0}&password={1}** 
Query used: **GetUserTokenQuery**
Responsibility: Get the JWT token for admin and other user. 
A dummy admin user credential is placed in the appsettings file.
With that credential admin user will get his JWT token which need to be passed as a Authorization header to access the following ***GET API# (2 & 3)*** which has only permission given for the admin user.
> Sample JWT token for admin user need to pass as http header:
> Key: Authorization
> Value: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwiUmVxdWlyZUFkbWluQWNjZXNzIjoiYWRtaW4tdXNlciIsImp0aSI6IjRhZGJjNDgxLWVjYjUtNGJjNS04ZGFhLTM3NTdhNTcwNjdhMyIsImV4cCI6MTU4MDY0OTM0MCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIn0.KCq7xnVWNPiE4PZkzY3CSJiRVzb5wqRz_uAVYsmsDBY 

(2) **/api/Tracking/CurrentTracking?registrationId={0}** 
Query used: **GetCurrentTrackingQuery**
Responsibility: Retrieve the current position of a vehicle by the vehicle RegistrationId. 

(3) **/api/Tracking/TrackingsInCertainTime?registrationId={0}&startTime={1}&endTime={2}** 
Query used: **GetTrackingsInCertainTimeQuery**
Responsibility:  Retrieve the positions of a vehicle during a certain time by the vehicle RegistrationId and a given date-time range. 
>Note that: DateTime need to provide upto second value to get the accurate result.

(4) **/api/Tracking/LocationName?latitude={0}&longitude={1}** 
Query used: **GetLocationNameQuery**
Responsibility: Retrieve the vehicle location for a given latitude and longitude.
This api use google map api to get the location name. 
> Note that: To use this API, you have to change the text: "YOUR GOOGLE API KEY" by the valid Google API key in the appsettings file.

(5) **api/Registration?deviceId={0}** 
Query used: **GetRegistrationResponseByDeviceIdHandler**
Responsibility:  Retrieve the RegistrationId by the Vehicle DeviceId.

## Others

- Design: Used CQRS pattern.
- Security: Used different type of JWT token to grant API access for different level of user permission.
-  Extensibility, Performance , Salability: In terms of considering these features compare to SQL, MongoDB is a better choice, which is why MongoDB is used here.

# VehicleTrackingClient - (Console App)    

This is a console application  written in .NET Core 2.2.
The goal is to test the VehicleTrackingAPI for the following endpoints. 
1. /api/Registration
2. /api/Tracking
3. /api/Token?userName={0}&password={1}
4. /api/Tracking/CurrentTracking?registrationId={0}
5. /api/Tracking/TrackingsInCertainTime?registrationId={0}&startTime={1}&endTime={2}

- It can register **n** number of devices
- It can store **n** number of positions per device

Configuration can be changed from: **Constants.cs**
 



