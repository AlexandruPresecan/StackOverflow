import { Injectable } from "@angular/core";
import { QuestionVote } from "../models/question-vote.model";
import { BaseService } from "./base-service.service";

@Injectable()
export class QuestionVotesService extends BaseService<number, QuestionVote> {
  route: string = "questionvotes";
}
