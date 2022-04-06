import { TestBed } from "@angular/core/testing";
import { QuestionsService } from "./questions.service";
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { Question } from "../models/question.model";

describe('QuestionsService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [QuestionsService]
    })
  });

  it('#questions should be received from backend',
    () => {
      TestBed.get(QuestionsService).getAll().subscribe((result: Question[]) => {
        expect(result.length > 0).toBeTrue();
      });
    });
});
