import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { apiUrl } from "../environments/environment";
import { Authentication } from "../models/authentication.model";
import { User } from "../models/user.model";
import { BaseService } from "./base-service.service";

@Injectable()
export class UsersService extends BaseService<string, User> {

  route: string = "applicationusers";

  public login(value: User): Observable<Authentication> {
    return this.http.post<Authentication>(apiUrl + this.route + "/authentication/login", value);
  }

  public register(value: User): Observable<string> {
    return this.http.post<string>(apiUrl + this.route + "/authentication/register", value, this.httpOptions);
  }

  public ban(id: string): Observable<string> {
    return this.http.put<string>(apiUrl + this.route + "/ban/" + id, null, this.httpOptions);
  }

  public unban(id: string): Observable<string> {
    return this.http.put<string>(apiUrl + this.route + "/unban/" + id, null, this.httpOptions);
  }
}
