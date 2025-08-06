# Errors.md

# First

System.InvalidOperationException: The instance of entity type 'School' cannot be tracked because another instance with the same key value for {'Id'} is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.
at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.IdentityMap`1.ThrowIdentityConflict(InternalEntityEntry entry)
   at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.IdentityMap`1.Add(TKey key, InternalEntityEntry entry, Boolean updateDuplicate)
at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.IdentityMap`1.Add(TKey key, InternalEntityEntry entry)
   at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.IdentityMap`1.Add(InternalEntityEntry entry)
at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.StartTracking(InternalEntityEntry entry)
at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.InternalEntityEntry.SetEntityState(EntityState oldState, EntityState newState, Boolean acceptChanges, Boolean modifyProperties)
at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.InternalEntityEntry.SetEntityState(EntityState entityState, Boolean acceptChanges, Boolean modifyProperties, Nullable`1 forceStateWhenUnknownKey, Nullable`1 fallbackState)
at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.EntityGraphAttacher.PaintAction(EntityEntryGraphNode`1 node)
   at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.EntityEntryGraphIterator.TraverseGraph[TState](EntityEntryGraphNode`1 node, Func`2 handleNode)
   at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.EntityGraphAttacher.AttachGraph(InternalEntityEntry rootEntry, EntityState targetState, EntityState storeGeneratedWithKeySetTargetState, Boolean forceStateWhenUnknownKey)
   at Microsoft.EntityFrameworkCore.Internal.InternalDbSet`1.SetEntityState(InternalEntityEntry entry, EntityState entityState)
at Microsoft.EntityFrameworkCore.Internal.InternalDbSet`1.Update(TEntity entity)
   at TwoSchools.Infra.Repositories.BaseRepository`1.UpdateAsync(T entity) in C:\Users\ahmed.ismail\source\repos\TwoSchools\TwoSchools.Infra\Repositories\BaseRepository.cs:line 43
at TwoSchools.App.Services.SchoolService.UpdateSchoolAsync(School school) in C:\Users\ahmed.ismail\source\repos\TwoSchools\TwoSchools.App\Services\SchoolService.cs:line 65
at TwoSchools.Controllers.SchoolController.UpdateSchool(Int32 id, UpdateSchoolRequest request) in C:\Users\ahmed.ismail\source\repos\TwoSchools\TwoSchools\Controllers\SchoolController.cs:line 48
at lambda_method174(Closure, Object)
at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(ActionContext actionContext, IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)
at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g**Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask)
at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g**Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeInnerFilterAsync>g**Awaited|13_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g**Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g**Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g**Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)
at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)
at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)
at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)

### HEADERS

Accept: text/plain
Host: localhost:7207
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36
Accept-Encoding: gzip, deflate, br, zstd
Accept-Language: en;q=0.9
Content-Type: application/json
Origin: https://localhost:7207
Referer: https://localhost:7207/swagger/index.html
Content-Length: 25
sec-ch-ua-platform: "Windows"
sec-ch-ua: "Not)A;Brand";v="8", "Chromium";v="138", "Brave";v="138"
DNT: 1
sec-ch-ua-mobile: ?0
sec-gpc: 1
sec-fetch-site: same-origin
sec-fetch-mode: cors
sec-fetch-dest: empty
priority: u=1, i

---
