import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { apiUrl } from "../environments/environment";

@Injectable()
export class BaseService<T> {

  protected route: string = "";

  constructor(private http: HttpClient) {

  }

  public getAll(): Observable<T[]> {
    return this.http.get<T[]>(apiUrl + this.route);
  }

  public getById(id: number): Observable<T> {
    return this.http.get<T>(apiUrl + this.route + '/' + id);
  }

  public create(value: T): Observable<T> {
    return this.http.post<T>(apiUrl + this.route, value);
  }

  public update(id: number, value: T): Observable<T> {
    return this.http.put<T>(apiUrl + this.route + '/' + id, value);
  }

  public delete(id: number): Observable<T> {
    return this.http.delete<T>(apiUrl + this.route + '/' + id);
  }
}

