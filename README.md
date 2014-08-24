MVC-Monitor
===========

A tool for reporting unhandled exceptions in real time for MVC applications, includes search/filter functionality and a live dashboard.

<h2>Configuration</h2>

<b>Applications</b>

MVC-Monitor needs to know which of your applications it will monitor. The app setting in web.config 'Applications' is where you tell it. It is a CSV list of 'sourceId's which you configured in the elmah/error post config value for each of your applications.

<b>Error repository</b>

This is where you select which sort of repository is used to store errors. MVC monitor has it's own store for quick access to any errors it receives. Out of the box, MVC monitor is configured to use an in memory repository to store errors. The down side with this method is that the errors are lost if the app pool is restarted. The application can be configured to use an NHibernate repository to persist the errors permanently in a database. 

<b>Configuring the NHibernate/SQL repository</b>

To install the database and configure the NHibernate repository:
* Create two new databases MvcMonitor and MvcMonitorTest, for live and tests.
* Run the baseline database script found in MVC-Monitor\MvcMonitor.WebApp\DatabaseMigrations
* In web.config, modify the connection string called MvcMonitor to point to the live database (MvcMonitor) you just created
* In web config change the setting under configuration / appSettings / ErrorRepository value to 'NHibernateRepository'
* In the test project open hibernate.cfg.xml and change the connection.connection_string property to point to the test database (MvcMonitorTest)
* Unignore all the tests under MvcMonitor.Tests/Repositories/NhibernateRepositoryTests
* All the unit tests should now pass and the applicaiton will save all it's data to the database
