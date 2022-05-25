import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { apiUrl } from "../environments/environment";

@Injectable()
export class BaseService<K, V> {

  protected route: string = "";
  protected httpOptions!: {};

  constructor(protected http: HttpClient) {
    this.httpOptions = {
      responseType: 'text',
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage["token"]
      })
    };
  }

  public getAll(): Observable<V[]> {
    return this.http.get<V[]>(apiUrl + this.route);
  }

  public getById(id: K): Observable<V> {
    return this.http.get<V>(apiUrl + this.route + '/' + id);
  }

  public create(value: V): Observable<string> {
    return this.http.post<string>(apiUrl + this.route, value, this.httpOptions);
  }

  public update(id: K, value: V): Observable<string> {
    return this.http.put<string>(apiUrl + this.route + '/' + id, value, this.httpOptions);
  }

  public delete(id: K): Observable<string> {
    return this.http.delete<string>(apiUrl + this.route + '/' + id, this.httpOptions);
  }
}

