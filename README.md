# COURSEger

COURSEger to bardzo lekki menadżer kursów, który umożliwia ich tworzenie oraz edycję. Pozwala także na przypisywanie do nich tematów zajęć.

## Technologie
NET .7 + Angular 15 + SQLite

## Instalacja i uruchamianie

### API
Należy utworzyć plik bazy danych SQLite komendą:
```
dotnet-ef database update --startup-project Web.Api --project Infrastructure.Persistence
```
Po tym kroku API jest gotowe do uruchomienia.
### Klient
Należy zaktualizować wszystkie pakiety występujące w projekcie:
```
npm install
```
Po tym kroku aplikacja kliencka jest gotowa do uruchomienia komendą:
```
npm start
```

**Ważne!**
Upewnij się, że adres umieszczony w `environment.ts` odpowiada adresowi Api.

## Jak to wygląda?
![Screenshot](https://i.imgur.com/gXJ4OpA.png)
