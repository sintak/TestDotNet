## Fiddler
+ Request: Composer
+ Response: Inspectors

+ POST响应码为201，表示资源创建成功。在响应头里有一个Location属性，这是一个导航属性，属性值是一个url地址，直接指向了刚刚Post成功的资源地址。
+ PUT为更新操作。按照规范，当服务更新操作执行成功后，直接通过响应码204告诉客户端调用成功，默认没有响应body。
+ DELETE为删除操作。按照规范，需要通过响应码判断是否成功（200）还是失败（500），默认没有响应body。


## Logger
在.Net Core框架里，日志功能主要由 ILoggerFactory, ILoggerProvider, ILogger 这三个接口体现：
ILoggerFactory：工厂接口。只提供注册LoggerProvider的方法和创建单实例Logger对象的方法。
ILoggerProvider：提供真正具有日志输出功能的Logger对象的接口。每一种日志输出方式对应一个不同的LoggerProvider类。
ILogger：Logger接口。Logger实例内部会维护一个ILogger接口的集合，集合的每一项都是由对应的LoggerProvider类注册生成的Logger对象而来。当调用Logger的日志输出方法时，实际是循环调用内部集合的每一个Logger对象的输出方法，所以就能看到上面出现的效果。


## Middleware
管道模型 vs 中间件

## Filter
概念：

#### 作为特性标识引用

#### 全局过滤器

#### 通过ServiceFilter引用

#### 通过TypeFilter引入

#### 自定义过滤器执行顺序
以ActionFilter执行顺序为例，默认执行顺序如下图:
1. Controller OnActionExecuting
2. Global OnActionExecuting
3. Class OnActionExecuting
4. Method OnActionExecuting
5. Method OnActionExecuted
6. Class OnActionExecuted
7. Global OnActionExecuted
8. Controller OnActionExecuted

#### 过滤器与中间件
1. 过滤器是MVC框架的一部分，中间件属于Asp.Net Core管道的一部分。
2. 过滤器在处理请求和响应时更加的精细一些，在用户权限、资源访问、Action执行、异常处理、返回值处理等方面都能进行控制和处理。而中间件只能粗略的过滤请求和响应。