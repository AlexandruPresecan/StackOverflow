import { Injectable } from "@angular/core";
import { Answer } from "../models/answer.model";
import { BaseService } from "./base-service.service";

@Injectable()
export class AnswersService extends BaseService<Answer> {
  route: string = "answers";
}
