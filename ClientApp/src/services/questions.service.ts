import { Injectable } from "@angular/core";
import { Question } from "../models/question.model";
import { BaseService } from "./base-service.service";

@Injectable()
export class QuestionsService extends BaseService<Question> {
  route: string = "questions";
}
