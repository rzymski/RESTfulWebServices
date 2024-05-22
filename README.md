___
**RESTfulWebServices**
___

# Konfiguracja
<details>
<summary><h3>Stworzenie pustej bazy danych</h3></summary>
  Tworzymy bazę danych za pomocą <b><code>SQL Server Object Explorer</code></b>.<br>
  View -> SQL Server Object Explorer -> SQL Server -> Database -> Add new Database<br>
  Po stworzeniu bazy danych, odświeżamy ją klikając na nią prawym przyciskiem i Refresh.<br>
  Nastepnie klikamy na nią prawym przyciskiem, wybieramy <b>Properties</b><br>
  z <b>Properties</b> kopiujemy wartość <b>Connection string</b>, które umieszczmy w <b><code>appsettings.json</code></b> w <b>"DefaultConnectionString"</b><br>
</details>
<details>
<summary><h3>Utworzenie tabel z Entities</h3></summary>
  Otwieramy <b><code>Package Manage Console</code></b><br>
  View -> Other Windows -> Package Manage Console<br>
  W <b>Package Manage Console</b> w <b><code>Default project:</code></b>  wybieramy <b><code>DB</code></b><br><br>
  
  Dodanie migracji
  ```sh
  add-migration InitialCreateDatabase
  ```
  Wdrożenie migracji i zaktualizowanie bazy danych
  ```sh
  update-database
  ```
</details>

<details>
<summary><h3>Utworzenie certifikatu ssl dla klienta w pythonie</h3></summary>
  Otwieramy <b>Windows Power shall jako admin</b><br>

  Sprawdzamy klucz certifikatu z visual studio za pomocą:
  ```sh
  dotnet dev-certs https --check
  ```
  
  Zapisujemy certifakt do zmiennej ważne żeby podać prawidłwowy klucz:
  ```sh
  $cert = Get-ChildItem -Path Cert:\CurrentUser\My | Where-Object {$_.Thumbprint -eq "twoj41ZnakowyKLuczOdczytanyZPoprzedniegoPolecenia"}
  ```
  
  Podajemy folder gdzie ma wyeksportować klucz i dowolne haslo :
  ```sh
  $path = "D:\pathToProject\pythonClient"
  Export-PfxCertificate -Cert $cert -FilePath "$path\localhost.pfx" -Password (ConvertTo-SecureString -String "twojeDowolneHaslo" -Force -AsPlainText)
  ```
  
  Otwieramy openssl np. w <b>C:\Program Files\Git\usr\bin\openssl.exe</b>
  ```sh
  pkcs12 -in D:\pathToProject\pythonClient\localhost.pfx -out D:\pathToProject\pythonClient\certificate.pem -nodes
  ```
  Po wpisaniu polecenia należy podać wcześniej wybrane hasło w tym przykładzie było to "twojeDowolneHaslo"
</details>