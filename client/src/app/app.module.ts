import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavbarComponent } from './components/navbar/navbar.component';
import { MaterialModule } from './_modules/material.module';
import { CarsListComponent } from './components/cars/cars-list/cars-list.component';
import { CarAddComponent } from './components/cars/car-add/car-add.component';
import { BreadcrumbModule } from 'xng-breadcrumb';
import { HomeComponent } from './components/home/home.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DateInputComponent } from './_forms/date-input/date-input.component';
import { SelectInputComponent } from './_forms/select-input/select-input.component';
import { SharedModule } from './_modules/shared.module';
import { CarEditComponent } from './components/cars/car-edit/car-edit.component';
import { UserAddComponent } from './components/users/user-add/user-add.component';
import { UsersListComponent } from './components/users/users-list/users-list.component';
import { UserEditComponent } from './components/users/user-edit/user-edit.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { LoginComponent } from './components/login/login.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { LogComponent } from './components/log/log.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './components/errors/not-found/not-found.component';
import { ServerErrorComponent } from './components/errors/server-error/server-error.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { TiresAddComponent } from './components/tires/tires-add/tires-add.component';
import { SelectInputTiresComponent } from './_forms/select-input-tires/select-input-tires.component';
import { SelectInputUserComponent } from './_forms/select-input-user/select-input-user.component';
import { TiresListComponent } from './components/tires/tires-list/tires-list.component';
import { InsurancesListComponent } from './components/insurances/insurances-list/insurances-list.component';
import { TiresEditComponent } from './components/tires/tires-edit/tires-edit.component';
import { CarsArchivalListComponent } from './components/cars/cars-archival-list/cars-archival-list.component';
import { SettingsAddComponent } from './components/settings/settings-add/settings-add.component';
import { SettingsEditComponent } from './components/settings/settings-edit/settings-edit.component';
import { InsuranceEditComponent } from './components/insurances/insurance-edit/insurance-edit.component';
import { ConfirmDialogComponent } from './_dialog/confirm-dialog/confirm-dialog.component';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CarsListComponent,
    CarAddComponent,
    HomeComponent,
    TextInputComponent,
    DateInputComponent,
    SelectInputComponent,
    CarEditComponent,
    UserAddComponent,
    UsersListComponent,
    UserEditComponent,
    LoginComponent,
    LogComponent,
    NotFoundComponent,
    ServerErrorComponent,
    HasRoleDirective,
    TiresAddComponent,
    SelectInputTiresComponent,
    SelectInputUserComponent,
    TiresListComponent,
    InsurancesListComponent,
    TiresEditComponent,
    CarsArchivalListComponent,
    SettingsAddComponent,
    SettingsEditComponent,
    InsuranceEditComponent,
    ConfirmDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    BreadcrumbModule,
    ReactiveFormsModule,
    SharedModule,
    NgxSpinnerModule

  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi:true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi:true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi:true}
  ],
  bootstrap: [AppComponent],
  entryComponents: [ConfirmDialogComponent]
})
export class AppModule { }
