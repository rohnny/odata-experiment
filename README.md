# OData experiment
Experimenting with OData using different versions of EF / OData / .NET

## Create EF core migrations
````
cd data
dotnet ef migrations add InitialCreate --context AppDbContext -s ../api --output-dir Migrations
````

## Data setup
````
insert into MembershipUsers values ('Rohnny', 'Moland', 'rmoland@gmail.com', getdate());
insert into Department values ('Construction');
insert into MembershipDepartment values (1, 1);
insert into Location values ('Sandnes');
insert into DepartmentLocation values (1, 1);
````

## Experimenting with OData queries
E.g query more than 2 levels deep:
````
http://127.0.0.1:5000/api/MembershipUsers(1)?$expand=Departments($expand=Department($expand=DepartmentLocation($expand=Location)))
````
