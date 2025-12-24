

//Linkto  How to write the first draft of a novel in 30 days.docx
//Linkto  Brainstorming your story ideas.docx
//Linkto  Days 1-6 - creating your preliminary outline with characters, setting and plot.docx
//Linkto  Worksheet 1  Character sketch 
//Linkto  Worksheet 2A  General setting sketch 
//Linkto  Worksheet 2B  Character setting sketch 
//Linkto  Worksheet 3  Research list 
//Linkto  Worksheet 4  Plot sketch 
//Linkto  Worksheet 5  Summary outline 
//Linkto  Worksheet 6  Miscellaneous scene notes 
//Linkto  Worksheet 7  Closing scene notes 
//Linkto  Worksheet 8  Interview questions 
//Linkto  Worksheet 9  Dialogue sheet 
//Linkto  Worksheet 10  Fact sheet 
//Linkto  Worksheet 11  Background timeline 
//Linkto  Worksheet 12  Miscellaneous timeline 
//Linkto  Worksheet 13  Story evolution 
//Linkto  Worksheet 14  Formatted outline capsule 
//Linkto  Worksheet 15  Day sheet
//How to Write a Book in 30 Days worksheets.docx
using System.Windows;

public class CharacterSketch
{
    //Linkto  Day 1 - character sketches.docx   T:\Projects\WriteBook\Day 1 - character sketches.docx
    public string Title { get; set; }
    public string CharacterName { get; set; }
    public string Nickname { get; set; }
    public string BirthDatePlace { get; set; }
    public string CharacterRole { get; set; }
    public string PhysicalDescriptions { get; set; }
    public string Age { get; set; }
    public string Race { get; set; }
    public string EyeColour { get; set; }
    public string HairColourStyle { get; set; }
    public string BuildHeightWeight { get; set; }
    public string SkinTone { get; set; }
    public string StyleOfDress { get; set; }
    public string CharacteristicsAndMannerisms { get; set; }
    public string PersonalityTraits { get; set; }
    public string Background { get; set; }
    public string InternalConflicts { get; set; }
    public string ExternalConflicts { get; set; }
    public string OccupationEducation { get; set; }
    public string MiscellaneousNotes { get; set; }
}
public class GeneralSettingSketch
{
    public string Title { get; set; }
    public string NameOfSetting { get; set; }
    public string CharactersLivingInRegionTimePeriod { get; set; }
    public string YearOrTimePeriod { get; set; }
    public string Season { get; set; }
    public string CityAndState { get; set; }
    public string MiscellaneousNotes { get; set; }
}
public class CharacterSettingSketch
{
    //Linkto  Day 2 - Setting sketches and research strategies.docx
    public string Title { get; set; }
    public string CharacterName { get; set; }
    public string GeneralSettingsForCharacter { get; set; }
    public string CharactersHome { get; set; }
    public string Surroundings { get; set; }
    public string CityOrTown { get; set; }
    public string Neighbourhood { get; set; }
    public string Street { get; set; }
    public string Neighbours { get; set; }
    public string Home { get; set; }
    public string HomeInterior { get; set; }
    public string CharactersWorkplace { get; set; }
    public string BusinessName { get; set; }
    public string TypeOfBusiness { get; set; }
    public string CityOrTownOfBusiness { get; set; }
    public string BusinessNeighbourhood { get; set; }
    public string BusinessStreet { get; set; }
    public string IndividualWorkspace { get; set; }
    public string COworkers { get; set; }
    public string MiscellaneousNotes { get; set; }
}
public class ResearchList
{
    public string Title { get; set; }
    public string MaterialToResearchForTheBook { get; set; }
}
public class PlotSketch
{
    //Linkto  Day 3 - plot sketches.docx
    //Linkto  Top of the Document
    public string Title { get; set; }
    public string StoryGoal { get; set; }
    public string RomanceThread_Optional { get; set; }
    public string SubplotThreads1 { get; set; }
    public string Additional { get; set; }
    public string PlotTension { get; set; }
    public string RomanticSexualTension { get; set; }
    public string Release { get; set; }
    public string Downtime { get; set; }
    public string BlackMoment { get; set; }
    public string Resolution { get; set; }
    public string AfterEffectsOfResolution { get; set; }
}
public class SummaryOutline
{
    //Linkto  Days 4 and 5 - the summary outline.docx
    //Linkto  Top of the Document
    public string Title { get; set; }
    public string AFreeformChronologicalSummaryOfAllIntroductoryScenesForTheBook { get; set; }
}
public class MiscellaneousSceneNotes
{
    //Linkto  Day 6 - miscellaneous scene notes and closing scene notes.docx
    //Linkto  Top of the Document
    public string Title { get; set; }
    public string AFreeformSummaryOfScenesAppearingInTheMiddlePortionOfTheBook { get; set; }
}
public class ClosingSceneNotes
{
    //Linkto  Day 6 - miscellaneous scene notes and closing scene notes.docx
    //Linkto  Top of the Document
    public string Title { get; set; }
    public string AFreeformSummaryOfScenesAppearingInTheClosingPortionOfTheBook { get; set; }
}
public class InterviewQuestions
{
    //Linkto  Days 7–13 - researching your novel.docx
    //Linkto  Top of the Document
    public string Title { get; set; }
    public string Interviewee { get; set; }
    public string Question1 { get; set; }
    public string ChaptersPagesWhereAnswerIsNeeded { get; set; }
    public string FactsOrInformationIMayNeedDuringTheInterview { get; set; }
    public string Answer { get; set; }
    public string Question2NSameAsQuestion1 { get; set; }

}
public class DialogueItem
{
    public string Character { get; set; }
    public string DialogueSpecifics { get; set; }
    public string OtherMannerismsOrCharacterTags { get; set; }
}
public class DialogueSheet
{
    //Linkto  Days 7–13 - researching your novel.docx
    //Linkto  Top of the Document
    public string Title { get; set; }
    List<DialogueItem> Dialog { get; set; }
    public string Character { get; set; }
}
public class FactSheet
{
    //Linkto  Days 7–13 - researching your novel.docx
    //Linkto  Top of the Document
    public string Title { get; set; }
    public string PageOrChapter { get; set; }
}
public class BackgroundTimeline
{
    //Linkto  Days 7–13 - researching your novel.docx
    //Linkto  Top of the Document
    public string Title { get; set; }
    public string PageOrChapter { get; set; }
}
public class MiscellaneousTimeline
{
    //Linkto  Days 7–13 - researching your novel.docx
    //Linkto  Top of the Document
    public string Title { get; set; }
    public string PageOrChapter { get; set; }
}
public class StoryEvolutionTheBeginning
{
    //Linkto  Days 14-15 - the evolution of your story.docx
    //Linkto  Top of the Document
    public string Title { get; set; }
    public string ConflictIsIntroducedDetailTheMajorConflict { get; set; }
    public string StoryGoalIsIntroducedDetailTheMajorStoryGoal { get; set; }
    public string CharactersAreOutfittedForTheirTasks { get; set; }
    public string ListAndBrieflyDescribeTheCharactersWhoWillBeInvolvedInReachingTheStoryGoalAndDefeatingTheConflict { get; set; }
    public string DetailEachCharactersStrengthsAndWeaknesses { get; set; }
    public string Additional { get; set; }
}
public class StoryEvolutionTheMiddle1
{
    //Linkto  Days 14-15 - the evolution of your story.docx
    //Linkto  Top of the Document
    public string Title { get; set; }
    public string CharactersDesignShorttermGoalsToReachTheStoryGoal { get; set; }
    public string Character1_BrieflyDescribeFirstShortermGoalAndHowCharacterWillReachIt { get; set; }
    public string Character2_optional_BrieflyDescribeFirstShortermGoalAndHowCharacterWillReachIt { get; set; }
    public string AdditionalCharacters_optional_BrieflyDescribeFirstShortermGoalAndHowCharactersWillReachIt { get; set; }
    public string QuestToReachTheStoryGoalBeginsBrieflyDetailTheEventsThatTakePlace { get; set; }
    public string FirstShorttermGoalsAreThwartedBrieflyDetailTheEventsThatTakePlace { get; set; }
    public string CharactersReactWithDisappointment { get; set; }
    public string Character1_BrieflyDescribeReaction { get; set; }
    public string Character2_optional_BrieflyDescribeReaction { get; set; }
    public string AdditionalCharacters_optional_BrieflyDescribeReaction { get; set; }
    public string StakesOfTheConflictAreRaised { get; set; }
    public string DetailNewStakesOfTheConflictAndHowTheyAffectAllSubplots { get; set; }
    public string CharactersReactToTheConflict { get; set; }
    public string Character1_BrieflyDescribeReactionToTheConflict { get; set; }
    public string Character2_optional_BrieflyDescribeReactionToTheConflict { get; set; }
    public string AdditionalCharacters_optional_BrieflyDescribeReactionToTheConflict { get; set; }
}
public class StoryEvolutionTheMiddle2
{
    //Linkto  Days 14_15 _ the evolution of your story.docx
    //Linkto  Top of the Document
    public string CharactersReviseOldOrDesignNewShortermGoals { get; set; }
    public string Character1BrieflyDescribeNewShortermGoalAndHowCharacterWillReachIt { get; set; }
    public string Character2_optional_BrieflyDescribeNewShortermGoalAndHowCharacterWillReachIt { get; set; }
    public string AdditionalCharacters_optional_BrieflyDescribeNewShortermGoalAndHowCharactersWillReachIt { get; set; }
    public string QuestToReachTheStoryGoalIsContinuedBrieflyDetailTheEventsThatTakePlace { get; set; }
    public string ShortermGoalsAreAgainThwartedBrieflyDetailTheEventsThatTakePlace { get; set; }
    public string CharactersReactWithDisappointment { get; set; }
    public string Character1BrieflyDescribeReaction { get; set; }
    public string Character2_optional_BrieflyDescribeReaction { get; set; }
    public string AdditionalCharacters_optional_BrieflyDescribeReaction { get; set; }
    public string StakesOfTheConflictAreRaisedDetailNewStakesOfTheConflictAndHowTheyAffectAllSubplots { get; set; }
    public string CharactersReactToTheConflict { get; set; }
    public string Character1BrieflyDescribeReactionToTheConflict { get; set; }
    public string Character2_optional_BrieflyDescribeReactionToTheConflict { get; set; }
    public string AdditionalCharacters_optional_BrieflyDescribeReactionToTheConflict { get; set; }
    //Items7Through10CanRepeatHere.ThisSectionOfTheCycleCanRepeatSeveralTimesThroughoutTheCourseOfYourNovelAsYourCharactersReadjustTheirShortermGoalsInOrderToMeetTheirObjectives { get; set; }
}
public class StoryEvolutionTheMiddle3
                        {
    //Linkto  Days 14_15 _ the evolution of your story.docx
    //Linkto  Top of the Document
    public string DowntimeBeginsDetailTheEventsThatLeadToDowntime { get; set; }
    public string Character1BrieflyDescribeReactionToTheseEvents { get; set; }
    public string Character2_optional_BrieflyDescribeReactionToTheseEvents { get; set; }
    public string AdditionalCharacters_optional_BrieflyDescribeReactionToTheseEvents { get; set; }
    public string CharactersReviseOldOrDesignNewShortermGoalsWithRenewedVigor { get; set; }
    public string Character1BrieflyDescribeDesperateShortermGoalAndHowCharacterWillReachIt { get; set; }
    public string Character2_optional_BrieflyDescribeDesperateShortermGoalAndHowCharacterWillReachIt { get; set; }
    public string AdditionalCharacters_optional_BrieflyDescribeDesperateShortermGoalAndHowCharactersWillReachIt { get; set; }
    public string TheQuestToReachTheStoryGoalContinuesButInstabilityAboundsBrieflyDetailEventsThatTakePlace { get; set; }
    public string TheBlackMomentBeginsBrieflyDetailTheEventsThatTakePlaceAndHowTheyAffectAllSubplots { get; set; }
    public string TheCharactersReactToTheBlackMoment { get; set; }
    public string Character1BrieflyDescribeReaction { get; set; }
    public string Character2_optional_BrieflyDescribeReaction { get; set; }
    public string AdditionalCharacters_optional_BrieflyDescribeReaction { get; set; }
}
public class StoryEvolutionTheEnd1
{
    //Linkto  Days 14_15 _ the evolution of your story.docx
    //Linkto  Top of the Document
    public string Title { get; set; }
    public string CharactersModifyShortermGoalsOneLastTime { get; set; }
    public string APivotalLife_changingEventOccursDetailThisEventAndHowItAffectsAllSubplots { get; set; }
    //public string CharactersModifyShortermGoalsOneLastTime { get; set; }
    public string Character1BrieflyDescribeFinalShortermGoalAndHowCharacterWillReachIt { get; set; }
    public string Character2_optional_BrieflyDescribeFinalShortermGoalAndHowCharacterWillReachIt { get; set; }
    public string AdditionalCharacters_optional_BrieflyDescribeFinalShortermGoalAndHowCharactersWillReachIt { get; set; }
    public string TheShowdownBeginsDetailTheShowdownIncludingAllMainCharactersWhoAreInvolved { get; set; }
    public string TheOppositionIsVanquishedAndTheConflictEndsDetailHowThisHappens { get; set; }
    public string TheStoryGoalIsAchievedDetailResolutionPlotAndAllSubplots { get; set; }
    public string Additional { get; set; }
}
public class StoryEvolutionTheEnd2
{
    //Linkto  Days 14_15 _ the evolution of your story.docx
    //Linkto  Top of the Document
    public string CharactersReactToTheResolutionOfThePlotAndSubplots { get; set; }
    public string Character1BrieflyDescribeReactionToTheEndOfTheConflict { get; set; }
    public string Character2_optional_BrieflyDescribeReactionToTheEndOfTheConflict { get; set; }
    public string AdditionalCharacters_optional_BrieflyDescribeReactionsToTheEndOfTheConflict { get; set; }
    public string TheRelationshipBlackMomentIsAddressed_romanceNovelsOnly { get; set; }
    public string Character1BrieflyDescribeReaction { get; set; }
    public string Character2_optional_BrieflyDescribeReaction { get; set; }
    public string CharactersReviseTheirLifeGoals { get; set; }
    public string Character1BrieflyDescribeLifeGoal { get; set; }
    public string Character2_optional_BrieflyDescribeLifeGoal { get; set; }
    public string AdditionalCharacters_optional_BrieflyDescribeLifeGoals { get; set; }
    public string PossibleRe_emergenceOfTheConflictOrOpposition { get; set; }
}
public class FormattedOutlineCapsule
{
    //Linkto  Days 16_24 _ Introducing the formatted outline.docx
    //Linkto  Day 16 _ How to begin organising your formatted outline.docx
    //Linkto  Day 17 _ incorporating story evolution elements.docx
    //Linkto  Day 18 _ incorporating character and setting sketches.docx
    //Linkto  Day 19 _ incorporating research.docx
    //Linkto  Days 20_23 _ brainstorming your formatted outline.docx
    //Linkto  Day 24 _ creating a day sheet.docx
    //Linkto  Top of the Document
    public string Title { get; set; }
    public string Day { get; set; }
    public string ChapterAndScene { get; set; }
    public string Point_of_view_Character { get; set; }
    public string AdditionalCharacters { get; set; }
    public string Location { get; set; }
    public string ApproximateTime { get; set; }
    public string FactsNecessary { get; set; }
    public string Notes { get; set; }
    public string Questions { get; set; }
    public string DraftOfScene { get; set; }
}
public class DaySheet
{
    //Linkto  Day 24 _ creating a day sheet.docx
    //Linkto  Top of the Document
    public string Title { get; set; }
    public string Day { get; set; }

}

