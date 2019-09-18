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
  - Make add/remove/test configuration work again
- Infrastructure
  - Create components for some of the elements: select, input
  - Create TemplateedComponents for some of the elements: List, Dialog
  - Make infrastructure behave more like UI mockups
- Database
  - ~~Create local database (mqtt.db)~~
  - ~~EF Migrations: https://medium.com/swlh/blazor-ef-core-a-simple-web-app-part-1-3c54380cf69a~~
  - ~~Change to FluentMigrator / Dapper because EF sucks balls~~
  - ~~Create tables for saving (connection) configurations~~
  - Create table(s) for saving currently subscribed topics in order to re-subscribe (on per configuration basis)
  - Expand tables to accommodate all the necessary settings
- UI
  - Inspo: 
    - https://css-tricks.com/snippets/css/a-guide-to-flexbox/
	- https://webdesign.tutsplus.com/tutorials/building-responsive-forms-with-flexbox--cms-26767
  - Drop down select of QoS items
  - ~~The 3 different 'forms' should be displayed in a nice coherent way~~
  - Incoming messages should be displayed with more information and in a more data-table like way
  - Move CSS into LESS file and optimize
 
