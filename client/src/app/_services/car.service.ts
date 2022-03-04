import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Car } from '../_models/car';
import { CarForList } from '../_models/carForList';


@Injectable({
  providedIn: 'root'
})
export class CarService {
baseUrl = environment.apiUrl
  constructor(private http: HttpClient) { }


  getCars(){
    return this.http.get<CarForList[]>(this.baseUrl + 'car')
  }

  getCarById(id : number){
    return this.http.get<Car>(this.baseUrl + 'car/' + id)
  }

  addCar(car : Car){
    return this.http.post(this.baseUrl + 'car', car)
  }

  updateCar(car: Car, id: number){
    return this.http.put(this.baseUrl + 'car/' + id, car)
  }

  getExcel(cars: CarForList[]): Observable<Blob>{
    return this.http.post(this.baseUrl + 'car/excel', cars, {responseType: 'blob'} )
  }

  getPdf(cars: CarForList[]): Observable<Blob>{
    return this.http.post(this.baseUrl + 'car/pdf', cars, {responseType: 'blob'} )
  }
}

