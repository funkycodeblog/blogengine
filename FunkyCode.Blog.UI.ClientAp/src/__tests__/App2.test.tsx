import React from 'react';
import ReactDOM from 'react-dom';

import UserFilterService  from '../services/UserFilterService'
import { FilterResult } from '../contracts/FilterResult';
import { User } from '../model/User'
import { MockFactory } from '../model/MockFactory';



describe('UserFilterService', () => {
   
    const user01 : User = MockFactory.GetUser(1, ["skill-1", "skill-2"], ["hobby-1", "hobby-2"]);
    const user02 : User = MockFactory.GetUser(2, ["skill-1", "skill-3"], ["hobby-3"]);
    const user03 : User = MockFactory.GetUser(3, ["skill-5", "skill-6"], ["hobby-5"]);

    const users : User[] = [ user01, user02, user03 ];

    it('Empty tag should return all users', () => {
        debugger;
        let result = UserFilterService.FilterUsers(users, "")
        expect(result.users.length).toBe(3);
    });

    it('Random tag should return no users', () => {
        let result = UserFilterService.FilterUsers(users, "osrijgorij")
        expect(result.users.length).toBe(0);
    });

    it('Exact tag should return 2 users', () => {
        let result = UserFilterService.FilterUsers(users, "skill-1")
        expect(result.users.length).toBe(2);
    });

    it('Exact tag should return 1 skill', () => {
        let result = UserFilterService.FilterUsers(users, "skill-1")
        expect(result.skills.length).toBe(1);
    });

    it('skill tag', () => {
        let result = UserFilterService.FilterUsers(users, "skill")
        expect(result.skills.length).toBe(5);
    });

});