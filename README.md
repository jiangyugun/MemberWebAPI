# MemberWebAPI
+ 練習-建立端口為GraphQL的會員管理系統
+ 本專案使用的是 ASP.NET Core 6

## Step 1. 必要套件安裝
+ dotnet ef 的全域工具，才能使用dotnet ef的指令
  ```
  dotnet tool install --global dotnet-ef
  ```
+ 安裝EF Core
  ```
  Install-Package Microsoft.EntityFrameworkCore.SqlServer –version 6.0.12
  Install-Package Microsoft.EntityFrameworkCore.Tools –version 6.0.12
  ```
+ 安裝JWT套件
  ```
  Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -version 6.0.12
  ```
+ 安裝HotChocolate
  ```
  Install-Package HotChocolate.AspNetCore -version 12.15.2
  Install-Package HotChocolate.AspNetCore.Authorization -version 12.15.2
  Install-Package HotChocolate.Data.EntityFramework -version 12.15.2
  ```
## Step 2. 設定appsettings.json
+ ```
  "ConnectionStrings": {
      "AuthContext": "{自行設定連線字串}"
    },
    "TokenSettings": {
      "Issuer": "localhost:7239",
      "Audience": "localhost:7239",
      "Key": "{自行設定鑰匙}"
    }
    ```

## 小筆記 
+ 使用指令產生EF實體
  ```
  Scaffold-DbContext Name=ConnectionStrings:EldPlatContext Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables ACCOUNT, COMPANY, GROUPS, PLANS 
  ```
  指令說明 : </br>
  Scaffold-DbContext <連線字串> <使用的資料庫提供者套件> -o<是否要輸出在另一個資料夾> <輸出的資料夾> -c<Context名稱> -f
  + -o : 與輸出資料夾沒有填的畫，會在當前目錄產生檔案。
  + -c : 與名稱Context沒有填的話，會以Database的名稱作為DbContext名稱。
  + -f : 則是宣告要複寫原有檔案。