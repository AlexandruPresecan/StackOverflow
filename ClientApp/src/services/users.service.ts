import { Injectable } from "@angular/core";
import { User } from "../models/user.model";
import { BaseService } from "./base-service.service";

@Injectable()
export class UsersService extends BaseService<User> {
  route: string = "applicationusers";
}
