import { TestBed } from "@angular/core/testing";
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { AnswersService } from "./answers.service";
import { Answer } from "../models/answer.model";

describe('AnswersService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AnswersService]
    })
  });

  it('#questions should be received from backend',
    () => {
      TestBed.get(AnswersService).getAll().subscribe((result: Answer[]) => {
        expect(result.length > 0).toBeTrue();
      });
    });
});
