# UserManagement

# User Management API

A secure and scalable User Management API designed to handle user authentication, authorization, role management, and security auditing.

## Features

* User registration and account management
* Secure authentication
* Role-Based Access Control (RBAC)
* User profile management
* Password management
* Administrative user management
* Security audit logging
* RESTful API architecture
* Input validation and error handling

## Security Features

This API includes a comprehensive security auditing system that records critical events, including:

* User login attempts
* Successful authentications
* Failed authentication attempts
* Password changes
* Account creation and deletion
* Role assignments and permission changes
* Administrative actions
* Security-related events

Audit records provide traceability and accountability for system activities.

## Technology Stack

* Backend: Entity FrameWork
* Database: Local (temporary)
* Authentication: Identity Framework, Identity DbContext

## Getting Started

### Prerequisites

* .NET 10 SDK
* Database Server
* Git

### Installation

1. Clone the repository

```bash
git clone https://github.com/Playmaker3099/UserManagement.git
```

2. Navigate to the project directory

```bash
cd UserManagement
```

3. Configure environment variables

Create an environment configuration file and provide the required settings:

```env
DATABASE_CONNECTION_STRING=
JWT_SECRET=
JWT_ISSUER=
JWT_AUDIENCE=
```

4. Run database migrations

```bash
# Migration command here
```

5. Start the application

```bash
# Startup command here
```

## API Endpoints

### Authentication

| Method | Endpoint           | Description         |
| ------ | ------------------ | ------------------- |
| POST   | /api/auth/register | Register a new user |
| POST   | /api/auth/login    | Authenticate user   |
| POST   | /api/auth/logout   | Logout user         |

### Users

| Method | Endpoint        | Description    |
| ------ | --------------- | -------------- |
| GET    | /api/users      | Get all users  |
| GET    | /api/users/{id} | Get user by ID |
| PUT    | /api/users/{id} | Update user    |
| DELETE | /api/users/{id} | Delete user    |

### Roles

| Method | Endpoint        | Description    |
| ------ | --------------- | -------------- |
| GET    | /api/roles      | Retrieve roles |
| POST   | /api/roles      | Create role    |
| PUT    | /api/roles/{id} | Update role    |
| DELETE | /api/roles/{id} | Delete role    |

### Audit Logs

| Method | Endpoint             | Description                  |
| ------ | -------------------- | ---------------------------- |
| GET    | /api/audit-logs      | Retrieve audit events        |
| GET    | /api/audit-logs/{id} | Retrieve audit event details |

## Authorization

The API uses Role-Based Access Control (RBAC) to restrict access to protected resources.

Example roles:

* Administrator
* Manager
* Standard User

## Audit Logging

The auditing subsystem records:

* Timestamp
* User Identifier
* Event Type
* Action Performed
* Resource Affected
* Outcome
* Additional Metadata

These logs can be used for:

* Security monitoring
* Compliance requirements
* Incident investigation
* Operational troubleshooting

## Error Handling

The API returns standardized HTTP status codes and error responses.

Example:

```json
{
  "success": false,
  "message": "Unauthorized access",
  "statusCode": 401
}
```

## Future Improvements

* Multi-factor authentication (MFA)
* Account lockout policies
* Refresh token support
* Permission-based authorization
* SIEM integration
* Advanced audit reporting

## License

This project is licensed under the MIT License.

## Author

[Your Name]
