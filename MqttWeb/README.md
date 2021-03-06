## Installation
```
git clone https://github.com/thakhisis/mqtt-web
cd mqtt-web/MqttWeb
dotnet run
```
browse to [https://localhost:5001/mqtt](https://localhost:5001/mqtt)

## Usage
Basic MQTT Client, connect to MQTT server, then publish / subscribe to topics with or without payload.

## Questions 
Hvad er korrekt måde hvis man skal opdatere 'random' komponenter på baggrund af property (list) change?
  - Fx. at et nav menu item skal vises / skjules på baggrund af en anden komponent?
    - Purpose: NavLink items i menu bliver opdateret når man subscriber til topic

## Todo
- Functionality 
  - ~~Subscribed topics should be displayed in the navmenu for easy navigation~~
- Infrastructure
  - Create components for some of the elements: select, input
  - Create TemplateedComponents for some of the elements: List, Dialog
- Database
  - ~~Create local database (mqtt.db)~~
  - EF Migrations: https://medium.com/swlh/blazor-ef-core-a-simple-web-app-part-1-3c54380cf69a
  - Create tables for saving (connection) configurations	
  - Create table(s) for saving currently subscribed topics in order to re-subscribe (on per configuration basis)
- UI
  - Drop down select of QoS items
  - ~~The 3 different 'forms' should be displayed in a nice coherent way~~
  - Incoming messages should be displayed with more information and in a more data-table like way

 
