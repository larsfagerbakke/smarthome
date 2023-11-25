# Welcome

Welcome to the SmartHome project. Why pick an existing project and solutions when you can create your own? I have too much free time...

## Overview

The basic thought behind this project is to create a sensor node that collects data. This could be temperature, humidity, light, and such. The sensor should then communicate with a backend, and this data will be presented in a web app. There can also be multiple specialized sensors, one for temperature and humidity, and another for just light.

## Project Structure

**PCB design(s)** live in the "circuit-diagrams" folder.

**Embedded code** resides in the "embedded" folder. The initial code will be Arduino, with the possibility down the road to migrate the code base over to Zephyr, for instance.

**3D files** for enclosures and such live in "mechanical-designs." Recently, I got myself an Ender 3 S1 3D printer that works great, so I really enjoy this aspect of the project too.

**Backend & frontend applications** will reside in the "server" folder. It will initially contain a shared library, backend, and frontend project. I'm a .NET developer by trade, so I'm not stepping out of my comfort zone here. Later on, there will be provisioning and other systems added to the solution.

### Security

In the initial phase, there will be no security. It will rely on the fact that the solution is hosted in a private (home) network and firewalled by a consumer router. But I do plan to support authentication and authorization, as I want a single backend supporting multiple locations, where traffic from a node goes through the internet.

## Roadmap

- ~~Project creation~~
- Scaffolding HTTP(s) data broker
- Scaffolding HTTP Arduino client
- Scaffolding user dashboard
- TODO ..

## Contributing

Feel free to reach out to me if you want to contribute to any part of the project. God-like circuit skills? Or is 3D design more your thing? Or do you want to code? Email is in my profile.
