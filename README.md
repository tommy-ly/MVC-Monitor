MVC-Monitor
===========

A tool for reporting unhandled exceptions in real time for MVC applications, includes search/filter functionality and a live dashboard.

<h2>Setup and configuration</h2>

<b>Configuring the NHibernate/SQL repository</b>

By default MVC monitor is configured to use an in memory repository to store errors. The down side is that the errors are lost if the app pool is restarted. The application can be configured to use an NHibernate repository to persist the errors in a database.

To install the database and configure the NHibernate repository:
* Create two new databases MvcMonitor and MvcMonitorTest, for live and tests.
* Run the baseline database script found in MVC-Monitor\MvcMonitor.WebApp\DatabaseMigrations
* Modify the connection string called MvcMonitor to point to the live database (MvcMonitor) you just created
* In web config change the setting under configuration / appSettings / ErrorRepository value to 'NHibernateRepository'
* In the test project open hibernate.cfg.xml and change the connection.connection_string property to point to the test database (MvcMonitorTest)
* Unignore all the tests under MvcMonitor.Tests/Repositories/NhibernateRepositoryTests
* All the unit tests should now pass and the applicaiton will save all it's data to the database
