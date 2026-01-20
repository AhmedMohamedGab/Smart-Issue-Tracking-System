# Requirements Specification

## Functional Requirements

FR-01: The system shall allow an Admin to create and manage users.

FR-02: The system shall support multiple user roles: Admin, Manager, and Developer.

FR-03: The system shall allow an Admin or a Manager to create projects.

FR-04: The system shall allow a Manager to create issues within a project.

FR-05: The system shall allow a Manager to assign issues to developers.

FR-06: The system shall allow a Developer to view issues assigned to them.

FR-07: The system shall support issue status transitions (Open, In Progress, In Review, Done, Closed).

FR-08: The system shall prevent invalid status transitions based on user role.

FR-09: The system shall track issue priority and due dates.

FR-10: The system shall generate reports such as overdue issues and developer performance summaries.


## Non-Functional Requirements

NFR-01: The system shall be implemented using C# and .NET.

NFR-02: The system shall follow SOLID principles and object-oriented design.

NFR-03: Business logic shall be separated from presentation logic.

NFR-04: Data persistence shall be abstracted using the Repository Pattern.

NFR-05: The system shall use file-based storage for data persistence.

NFR-06: The system shall be console-based.