import { TestBed } from "@angular/core/testing";
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { TagsService } from "./tags.service";
import { Tag } from "../models/tag.model";

describe('TagsService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [TagsService]
    })
  });

  it('#tags should be received from backend',
    () => {
      TestBed.get(TagsService).getAll().subscribe((result: Tag[]) => {
        expect(result.length > 0).toBeTrue();
      });
    });
});
