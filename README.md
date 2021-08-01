# MyCMS
MyCMS is a content management system that provides user an ease to edit website content with no coding knowledge, once the application setup completes. We can add pages, PDF file and edit content from control panel application and the same will generate the page content, PDF download link and text content respectively on website. 

### Applications:
- ServiceWebsite
- ControlPanel


### Features:
- Manage client logo 
- Create and manage modules(Services, Products, etc) dynamically 
- Add dynamic pages up to 3 levels for a module created 
- File upload functionality with auto generated download links
- Manage content on the fly.
- Email nofification for contact and careers section


### Environment:
- Visual Studio 2017 or the later.
- Dotnet Framework:  4.7.1
- MS SQL Server 2008 R2 or above. (2016 Preferred)

### Third party tool/library used:
#### For Maps:
https://www.embedmymap.com/

#### For visitors count:
https://www.freecounterstat.com/

#### For logging errors:
Nlog - https://nlog-project.org/

#### Theme used for control panel:
AdminLTE - https://adminlte.io/


### Project setup:
#### Starter:
- Execute MyCMS.sql(Stored in folder named SQL) to setup database.
- Update ConnectionString details in web.config of MyCMS website and control panel application.
- Right click on project -> Properties -> Web -> Click on create virtual directory. (Do this step for ServiceWebsite and Controlpanel in solution explorer). 
- Once above steps are done you are good to go.
- You can Setup Email configuration for contact page and careers page later on.

**Note: Incase you create custom virtual directory then you need to do the relevant change in web.config file of ServiceWebsite for image URL and pdf URL.
Make sure project folder has write access for IIS_IUSRS, else image save and PDF save will fail.** 

#### Advanced user:
- You can create new modules by following below steps (Suppose New Module to be Scheme)
	- Make an entry in Category table -> insert into Category values ('Scheme')
	- Create an entry of Scheme in ServiceWebsite's enum Category(CategoryViewModel)  -> eg: public enum Category{Services =1, Products=2, Scheme =3 }.  -> here make sure the value assigned to the Scheme should id in Category table in database.
	- Create controller for Scheme and copy actions (Index, Section and SubSection) from ServicesController and paste it in the Scheme controller. Replace the Category enum value in index action of Scheme controller with Category.Scheme.
	- Copy index.cshtml, Section.cshtml and SubSection.cshtml from Views -> Services, create a folder Named Scheme  in Views folder and paste.
	- Build the application and you are good to go.
	
	
### Visuals:
**1. Create and manage content, dynamic pages and upload files from control panel application.**

![alt text](https://github.com/bhimpratapsingh/MyCMS/blob/main/ReadMeImages/CreateType.gif?raw=true)


**2. Manage client logo section**

![alt text](https://github.com/bhimpratapsingh/MyCMS/blob/main/ReadMeImages/AddLogo.gif?raw=true)


**3. Manage box color for modules created on homepage**

![alt text](https://github.com/bhimpratapsingh/MyCMS/blob/main/ReadMeImages/BoxColor.gif?raw=true)
