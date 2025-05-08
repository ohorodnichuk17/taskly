export interface IAchievementsInitialState {
    achievements: IAchievement[] | null,
    newAchievement: INewAchievement | null
}

export interface IAchievement {
    id: string,
    name: string,
    description: string,
    reward: number,
    percentageOfCompletion: number,
    icon: string,
    isCompleated: boolean
}

export interface INewAchievement {
    id: string,
    name: string,
    icon: string
}