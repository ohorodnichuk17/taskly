export interface IGamificationInitialState {
    challenges: IChallengeResponse[] | null;
    availableChallenges: IChallengeResponse[] | null;
    activeChallenges: IChallengeResponse[] | null;
    badges: IBadgeResponse[] | null;
    userBadges: IUserBadgeResponse[] | null;

    isLoadingChallenges: boolean;
    isLoadingBadges: boolean;
    isLoadingUserBadges: boolean;
    isCreatingChallenge: boolean;
    isDeletingChallenge: boolean;
    error: string | null;
}

export interface ICreateChallengeRequest {
    name: string;
    description: string;
    startDate: string;
    endDate: string;
    points: number;
    isActive: boolean;
    ruleKey: string;
    targetAmount: number;
}

export interface ICreateChallengeResponse {
    id: string;
}

export interface IChallengeResponse {
    id: string;
    name: string;
    description: string;
    startDate: string;
    endDate: string;
    isCompleted: boolean;
    isBooked: boolean;
    isActive: boolean;
    ruleKey: string;
    points: number;
    targetAmount: number;
    userId?: string;
    user?: IUserForChallengeResponse;

}

export interface IUserForChallengeResponse {
    userName?: string;
    avatarId: string;
}


export interface IBadgeResponse {
    id: string;
    name: string;
    icon: string;
    requiredTasksToReceiveBadge: number;
    level: number;
}

export interface IUserBadgeResponse {
    badge: IBadgeForUserBadgeResponse;
    dateAwarded: string;
}

export interface IBadgeForUserBadgeResponse {
    name: string;
    icon: string;
    requiredTasksToReceiveBadge: number;
}