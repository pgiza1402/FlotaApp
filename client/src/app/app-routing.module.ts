import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarAddComponent } from './components/cars/car-add/car-add.component';
import { CarEditComponent } from './components/cars/car-edit/car-edit.component';
import { CarsArchivalListComponent } from './components/cars/cars-archival-list/cars-archival-list.component';
import { CarsListComponent } from './components/cars/cars-list/cars-list.component';
import { NotFoundComponent } from './components/errors/not-found/not-found.component';
import { ServerErrorComponent } from './components/errors/server-error/server-error.component';
import { HomeComponent } from './components/home/home.component';
import { InsuranceEditComponent } from './components/insurances/insurance-edit/insurance-edit.component';
import { InsurancesListComponent } from './components/insurances/insurances-list/insurances-list.component';
import { LogComponent } from './components/log/log.component';
import { LoginComponent } from './components/login/login.component';
import { SettingsAddComponent } from './components/settings/settings-add/settings-add.component';
import { SettingsEditComponent } from './components/settings/settings-edit/settings-edit.component';
import { TiresAddComponent } from './components/tires/tires-add/tires-add.component';
import { TiresEditComponent } from './components/tires/tires-edit/tires-edit.component';
import { TiresListComponent } from './components/tires/tires-list/tires-list.component';
import { UserAddComponent } from './components/users/user-add/user-add.component';
import { UserEditComponent } from './components/users/user-edit/user-edit.component';
import { UsersListComponent } from './components/users/users-list/users-list.component';
import { AdminGuard } from './_guards/admin.guard';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {path: '', component: HomeComponent, data: {breadcrumb : 'Strona Główna'} },
  {path: 'login', component: LoginComponent, data: {breadcrumb : 'Logowanie'} },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: "cars", component: CarsListComponent, canActivate: [AdminGuard], data: {breadcrumb : 'Lista pojazdów'}},
      {path: 'archival-cars', component: CarsArchivalListComponent, canActivate: [AdminGuard], data: {breadcrumb : 'Lista pojazdów archiwalnych'}},
      {path: 'add-car', component: CarAddComponent, canActivate: [AdminGuard], data: {breadcrumb: 'Dodawanie pojazdu'}},
      {path: 'car/:id', component: CarEditComponent, data: {breadcrumb: {alias: 'carName'}}},
      {path: 'users', component: UsersListComponent, canActivate: [AdminGuard], data: {breadcrumb : 'Lista użytkowników'}},
      {path: 'add-user', component: UserAddComponent, canActivate: [AdminGuard], data: {breadcrumb: 'Dodawanie użytkownika'}},
      {path: 'user/:id', component: UserEditComponent, canActivate: [AdminGuard], data: {breadcrumb : {alias: 'userName'}}},
      {path: 'logs', component: LogComponent, canActivate: [AdminGuard], data: {breadcrumb : 'Lista zdarzeń'}},
      {path: 'tires', component: TiresListComponent, canActivate: [AdminGuard], data: {breadcrumb : 'Lista opon'}},
      {path: 'add-tires', component: TiresAddComponent, canActivate: [AdminGuard], data: {breadcrumb : 'Dodawanie opon'}},
      {path: 'tires/:id', component: TiresEditComponent, canActivate: [AdminGuard], data: {breadcrumb : {alias: 'model'}}},
      {path: 'insurances', component: InsurancesListComponent, canActivate: [AdminGuard], data: {breadcrumb : 'Lista polis'}},
      {path: 'insurance/:id', component: InsuranceEditComponent, canActivate: [AdminGuard], data: {breadcrumb : {alias: 'name'}}},
      {path: 'settings', component: SettingsAddComponent, canActivate: [AdminGuard], data: {breadcrumb : 'Edycja kategorii'}},
      {path: 'settings/:id', component: SettingsEditComponent, canActivate: [AdminGuard], data: {breadcrumb : {alias: 'name'}}},
    ]
  },
  {path: 'not-found', component: NotFoundComponent, data: {breadcrumb : 'Nie znaleziono strony'}},
  {path: 'server-error', component: ServerErrorComponent, data: {breadcrumb : 'Błąd wewnętrzny serwera'}},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
