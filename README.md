## Windows Presentation Foundation - Mouse Controller
### Description
The project builds a WPF application that would simulate a set of mouse events in Windows operating system. Users are able to setup the controller using five commands sequentially and indicate the intervals between the commands. The tool is designed for automation in games and routine works.


### Build
|Project Dependency|[Visual Studio 2015](https://docs.microsoft.com/zh-tw/visualstudio/productinfo/vs2015-sysrequirements-vs#vs)|
|:-:|:-:|
|Framework|[Microsoft .Net 4.6.1](https://support.microsoft.com/zh-tw/help/3102436/the-net-framework-4-6-1-offline-installer-for-windows)|
|Graphical User Interface|[Windows Presentation Foundation](https://docs.microsoft.com/zh-tw/dotnet/framework/wpf/getting-started/walkthrough-my-first-wpf-desktop-application)|
|Dynamic Link Library|[win32.dll](https://www.dll-files.com/win32.dll.html)|


### Run
#### Requirement
The prebuild executable file `MouseController.exe` is available for Windows 7, Windows 8 and Windows 10.


#### Usage
![Imgur](https://i.imgur.com/cP0PGib.png)

|Index|Field|Description|
|:-:|:-:|:-:|
|1|Hints|The commands and the format of inputs are indroduced here.|
|2|Input|Commands are typed in this text area. For example, "SET_100_50 CLICK" would set the mouse cursor to position (100, 50) and trigger click event. Users could find the coordinate of any position from the status bar instantaneously.|
|3|Launch|Before starting the simulator, users may indicate the time interval between commands and specify how many times the overall script are supposed to be executed. The mouse would be controlled once the button is clicked. On the other hand, any manual disturbance will interrupt the controller and stop the simulation immediately.|
|4|Status|Status bar shows the current position of the user's cursor and the information during execution. When the simulator is working, the color of the fonts would be changed to red.|


### Demo
![Imgur](https://i.imgur.com/qpUNGWx.gif)


