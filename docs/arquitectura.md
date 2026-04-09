# Arquitectura de TaskFlow

Esta solución se organiza en tres capas principales:

- `src/TaskFlow/Models`: contiene los modelos de dominio, como `TaskItem`.
- `src/TaskFlow/Services`: contiene la lógica de negocio y operaciones con tareas, como `TaskService`.
- `src/TaskFlow/Utils`: contiene utilidades de consola y helpers generales.

También incluye un proyecto de pruebas en `tests/TaskFlow.Tests` para validar la lógica de `TaskService`.
