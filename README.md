MVC-Monitor
===========

A tool for reporting unhandled exceptions in real time for MVC applications, includes search/filter functionality and a live dashboard.

<h4>How does it work?</h4>

MVC Monitor can receive errors through the popular error catching tools ELMAH and ElmahR. ELMAH catches any unhandled exceptions just before they get thrown from an MVC applicaiton and reports it. The ElmahR extension then posts a JSON representation of the error to a URL of you choice. MVC monitor can receive the posted error and store it. All the errors MVC monitor receives can be seen arriving in real time on the dashboard or searched through via the index.

<h4>How do I start the monitor?</h4>

MVC Monitor is an MVC 4 application which can be hosted on any windows machine using IIS. Simply build the solution, check the tests run and host the application on your server or in the cloud.

<h4>What can I configure?</h4>

*Applications*

MVC-Monitor needs to know which of your applications it will monitor. The app setting in web.config 'Applications' is where you tell it. It is a CSV list of 'sourceId's which you configured in the elmah/error post config value for each of your applications.

*Error repository*

This is where you select which sort of repository is used to store errors. MVC monitor has it's own store for quick access to any errors it receives. Out of the box, MVC monitor is configured to use an in memory repository to store errors. The down side with this method is that the errors are lost if the app pool is restarted. The application can be configured to use an NHibernate repository to persist the errors permanently in a database. 

*Configuring the NHibernate/SQL repository*

To install the database and configure the NHibernate repository:
* Create two new databases MvcMonitor and MvcMonitorTest, for live and tests.
* Run the baseline database script found in MVC-Monitor\MvcMonitor.WebApp\DatabaseMigrations
* In web.config, modify the connection string called MvcMonitor to point to the live database (MvcMonitor) you just created
* In web config change the setting under configuration / appSettings / ErrorRepository value to 'NHibernateRepository'
* In the test project open hibernate.cfg.xml and change the connection.connection_string property to point to the test database (MvcMonitorTest)
* Unignore all the tests under MvcMonitor.Tests/Repositories/NhibernateRepositoryTests
* All the unit tests should now pass and the applicaiton will save all it's data to the database
