<h2>Project Description</h2>

**ASP.NET Core 2.2 MyPlace web application allows Restaurant / Hotel customers and managers to log comments about the service.**

Customers are able to **add comments** with positive/negative service feedback and view others&#39; feedback.

**Managers can take notes (logs)** about things happening on specific shift – for example unusual things, stuff related issues, TODOs, and sharing them between managers.

These **logs are organized into LogBooks** (they could be per Hotel, Restaurant in the hotel, the Spa in the hotel, etc.), and also can have categories/tags (like TODO, Maintenance, Events, etc.), logged for specific date and time.

The application has:

- **public part** (accessible without authentication)
- **private part** (available for managers)
- **administrative part** (available for administrator staff only)

<h2>Public Part</h2>

The **public part** of the applicationis **visible without authentication.**

It is the home page where all business entities (hotels, restaurants, etc) are visible for the customer. It provides auto-search capabilities.

The customers can give comments with positive and negative feedback and can review the comments by other customers in **real-time** which is realized with **SignalR**.

There is an **auto- filter** which which does not allow comments insulting words to be save.

<h2>Private Part</h2>

<h3>Managers</h3>

Managers have private page Notes accessible after **successful login**.
Notes page provides:

1. **Easy navigation** between all manager&#39;s **logbooks**.
2. **List** f all logbook manager&#39;s with soonest notes **on the top**.
3. Rapidly **adding notes with minimum clicks** and optionally giving entity specific tags.
4. **Advance search** by note text, tag, creator, exact date, from date, to date
5. Paging and sorting.

<h3>Administrators</h3>

**System administrators** have permissions to manage the accounts of other system administrators, managers and feedback moderators.

They are able to:

1. **Initialize new LogBooks** and **Tags** for them and
2. Give **access** for available **managers** to each **LogBook**.
3. Give **moderators** access to specific **business entities customer posts**.
4. View event logs


<h3>Moderators</h3>

The moderators have permissions for specific business entities. In this entities they can censor posts that does not comply with common sense rules (rude, swearing, etc.)

Тechnologies, frameworks and development techniques

<h3>Technologies, frameworks and development techniques:</h3>

- **ASP.NET Core 2.2**
- **Razor** template engine is used for generating the UI with
  - **sections** and **partial views**  **and**
  - **tag helpers**
- **MS SQL Server** for database back-end
  - **Entity Framework Core 2.2 code first**
  - **Used**  **service layer**
- **2 areas** in project : Administration and Notes for Managers access
- **Server-side paging and sorting**
- **Responsive UI** with **Bootstrap** including mobile device screens compatibility
- **Custom**  **Identity System** for managing users and roles. The registered users have one of these roles: **manager**, **administrator**  **or**  **moderator.**
- Used **AJAX form** communication **caching** f data when loading the date in the search autocomplete textbox in the home page.
- Applied **error handling** and **data validation** both client-side and server-side
- Create  **unit tests**  for your &quot;business&quot; functionality following the best practices for writing unit tests ( **at least 70% code coverage** )
- Use **Dependency Inversion** principle and **Dependency Injection** The application is integrated with **Azure Devops** as a **Continuous Integration server.** Theunit tests are configured to run on each commit to the master branch.
- Used GitHub and **branches** for writing different features.
- Hosted the application in **Azure https://myplace.azurewebsites.net/**
