import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { apiUrl } from "../environments/environment";
import { Question } from "../models/question.model";
import { BaseService } from "./base-service.service";

@Injectable()
export class QuestionsService extends BaseService<Question> {

  route: string = "questions";

  public getByTag(tag: string): Observable<Question[]> {
    return this.http.get<Question[]>(apiUrl + this.route + '?tag=' + tag);
  }
}
