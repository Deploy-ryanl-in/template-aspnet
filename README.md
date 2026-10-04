# ASP.NET Core Personal PaaS template

在 GitHub 点击 **Use this template → Create a new repository**，owner 选择 `Deploy-ryanl-in`，可选公有或私有。

```sh
git clone https://github.com/Deploy-ryanl-in/你的仓库.git
cd 你的仓库
dotnet restore --locked-mode
dotnet run --project src/App
```

需要 .NET SDK 10.0.401。开发完成后 commit、push 到 `paas.json` 声明的部署分支（默认 main）。创建仓库的初始提交与后续 push 均会触发测试、GHCR 构建和 OIDC 部署，成功后通过 `仓库名.ryanl.in` 访问。模板自身的 push 只执行测试。

默认示例无需密钥。`dotnet format --verify-no-changes`、`dotnet build -c Release`、`dotnet test -c Release` 与 CI 一致。后续用 `git pull` 同步自己的仓库；模板更新不会自动改写生成的独立仓库。

修改部署需求时编辑 `paas.json`。`name`、`domain` 的 `auto` 使用新仓库身份；仓库重命名保留原域名。配置必须通过本地 Schema 和服务器严格验证，不能声明宿主机命令或任意挂载。

有应用密钥时，在仓库 **Settings → Secrets and variables → Actions** 新增 `PAAS_SECRETS`，值为 JSON，例如 `{"POSTGRES_PASSWORD":"你的随机密码"}`，并在 `secretRefs` 引用。请勿提交 `.env`、密码、SSH key、Cloudflare token。密钥只在运行时注入。

`examples/postgres-redis.json` 和 `examples/multi-container.json` 展示数据库、Redis 和 Worker。示例 API 不自动连接数据库，需要自行添加应用数据访问逻辑。服务通过声明的服务名连接，每个仓库网络独立。PostgreSQL/Redis 的镜像更新需使用专用管理操作。768 MiB 总预算包含更新候选；资源不足会保留在线版本并拒绝更新。

在 **Actions → PaaS operations → Run workflow** 查看状态、日志、历史、回滚、停用、备份/恢复和数据库升级。停用保留卷。恢复必须填写 `RESTORE <repository ID>`；代码回滚不会回滚数据库写入。

中央 workflow 和第三方 Actions 固定 commit SHA；Dependabot 每周通过 PR 更新依赖。初次发布须由平台管理员完成服务器凭据配置。个人账号仓库需要 repository ID 白名单；组织内新仓库自动接入。
