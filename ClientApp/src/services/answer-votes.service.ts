import { Injectable } from "@angular/core";
import { AnswerVote } from "../models/answer-vote.model";
import { BaseService } from "./base-service.service";

@Injectable()
export class AnswerVotesService extends BaseService<number, AnswerVote> {
  route: string = "answervotes";
}
