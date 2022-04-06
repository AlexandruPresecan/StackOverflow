import { TestBed } from "@angular/core/testing";
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { UsersService } from "./users.service";
import { User } from "../models/user.model";

describe('UsersService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [UsersService]
    })
  });

  it('#users should be received from backend',
    () => {
      TestBed.get(UsersService).getAll().subscribe((result: User[]) => {
        expect(result.length > 0).toBeTrue();
      });
    });
});
