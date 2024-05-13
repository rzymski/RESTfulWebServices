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
