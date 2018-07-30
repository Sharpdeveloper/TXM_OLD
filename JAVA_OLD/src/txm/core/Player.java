/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package txm.core;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;
import java.util.function.BiFunction;

/**
 *
 * @author Sharpdeveloper
 */
public class Player{
    //static Bundle with the language, so it can be easy replaced and reffered
    private static int currentID = 0;
    private static Random random = new Random();
    private static Player Bye;
    public static Player getBye(){
        return (Bye == null ? Bye = new Player(java.util.ResourceBundle.getBundle("txm/resources/languages").getString("BYE")) : Bye);
    }
    private static Player WonBye;
    public static Player getWonBye(){
        return (WonBye == null ? WonBye = new Player(java.util.ResourceBundle.getBundle("txm/resources/languages").getString("WONBYE")) : WonBye);
    }

    private static int displayedRound = 0;
    public static void setDisplayedRound(int newDisplayedRound){
        displayedRound = newDisplayedRound;
    }

    
    //lastName
    private String lastName;
    public String getLastName(){
        return (lastName == null ? lastName = "" : lastName); //NOI18N
    }
    public void setLastName(String newLastName)    {
        lastName = newLastName;
    }
    //foreName
    private String foreName;
    public String getForeName(){
        return (foreName == null ? foreName = "" : foreName); //NOI18N
    }
    public void setForeName(String newForeName)    {
        lastName = newForeName;
    }
    //nickName
    private String nickName;
    public String getNickName(){
        return (nickName == null ? nickName = "" : nickName); //NOI18N
    }
    public void setNickName(String newNickName)    {
        lastName = newNickName;
    }
    //team
    private String team;
    public String getTeam(){
        return (team == null ? team = "" : team); //NOI18N
    }
    public void setTeam(String newTeam)    {
        lastName = newTeam;
    }
    //city
    private String city;
    public String getCity(){
        return (city == null ? city = "" : city); //NOI18N
    }
    public void setCity(String newCity)    {
        lastName = newCity;
    }
    //faction
    private String faction;
    public String getFaction(){
        return (faction == null ? faction = "" : faction); //NOI18N
    }
    public void setFaction(String newFaction){
        faction = newFaction;
    }
    
    //ID
    private int ID;
    public int getID(){
        return ID;
    }
    //T3ID
    private int T3ID;
    //rank
    private List<Integer> rank;
    public int getRank(){
        return (displayedRound == 0 ? ID : getRank(displayedRound));
    }
    public int getRank(int round){
        return rank.get(round);
    }
    //randomOrder
    public int getRandomOrder(){
        return random.nextInt(999999);
    }
    //wins
    private List<Integer> wins;
    public int getWins()    {
        return getWins(displayedRound);
    }
    public int getWins(int round){
        return wins.get(round);
    }
    //modifiedWins
    private List<Integer> modifiedWins;
    public int getModifiedWins()    {
        return getModifiedWins(displayedRound);
    }
    public int getModifiedWins(int round){
        return modifiedWins.get(round);
    }
    //draws
    private List<Integer> draws;
    public int getDraws()    {
        return getDraws(displayedRound);
    }
    public int getDraws(int round){
        return draws.get(round);
    }
    //modifiedLosses
    private List<Integer> modifiedLosses;
    public int getModifiedLosses()    {
        return getModifiedLosses(displayedRound);
    }
    public int getModifiedLosses(int round){
        return modifiedLosses.get(round);
    }
    //losses
    private List<Integer> losses;
    public int getLosses()    {
        return getLosses(displayedRound);
    }
    public int getLosses(int round) {
        return losses.get(round);
    }
    //tournamentPoints
    private List<Integer> tournamentPoints;
    public int getTournamentPoints(){
        return getTournementPoints(displayedRound);
    }
    public int getTournementPoints(int round){
        return tournamentPoints.get(round);
    }
    //destroyedPoints
    private List<Integer> destroyedPoints;
    public int getDestroyedPoints(){
        return getDestroyedPoints(displayedRound);
    }
    public int getDestroyedPoints(int round){
        return destroyedPoints.get(round);
    }
    //lostPoints
    private List<Integer> lostPoints;
    public int getLostPoints(){
        return lostPoints;
    }
    //strengthOfSchedule
    private List<Integer> strengthOfSchedule;
    public int getStrengthOfSchedule(){
        return strengthOfSchedule;
    }
    //marginOfVictory
    private List<Integer> marginOfVictory;
    public int getMarginOfVictory(){
        return marginOfVictory;
    }
    //tableNo
    private int tableNo = 0;
    public int getTableNo(){
        return tableNo;
    }
    public void setTableNo(int newTableNo){
        tableNo = newTableNo;
    }
    //bye
    private int bye = 0;
    public boolean hasBye(){
        return bye != 0;
    }
    public boolean hasByeInRound(int round){
        return bye == round;
    }
    public void setBye(int round){
        bye = round;
    }
    
    
    //disqualified
    private boolean disqualified = false;
    public boolean isDisqualified(){
        return disqualified;
    }
    //present
    private boolean present = false;
    public boolean isPresent(){
        return present;
    }
    public void setPresent(boolean isPresent){
        present = isPresent;
    }
    //dropped
    private boolean dropped = false;
    public boolean hasDropped(){
        return dropped;
    }
    //wonBye
    private boolean wonBye = false;
    public boolean hasWonBye(){
        return wonBye;
    }
    //paired
    private boolean paired = false;
    public boolean isPaired(){
        return paired;
    }
    public void setPaired(boolean isPaired){
        paired = isPaired;
    }
    //paid
    private boolean paid = false;
    public boolean hasPaid(){
        return paid;
    }
    public void setPaid(boolean hasPaid){
        paid = hasPaid;
    }
    //listGiven
    private boolean listGiven = false;
    public boolean hasListGiven(){
        return listGiven;
    }
    public void setListGiven(boolean hasListGiven){
        listGiven = hasListGiven;
    }
    
    
    //enemies
    private List<Player> enemies;
    public List<Player> getEnemies(){
        return (enemies == null ? enemies = new ArrayList() : enemies);
    }
    //results
    private List<Result> results;
    public List<Result> getResults(){
        return (results == null ? results = new ArrayList() : results);
    }
    
    //Constructors
    public Player(String _nickName, String _lastName, String _foreName, String _team, String _city, String _faction, int _T3ID, boolean _paid, boolean _listGiven){
        //inialising
        ID = currentID++;
        
        //copying
        setNickName(_nickName);
        setLastName(_lastName);
        setForeName(_foreName);
        setTeam(_team);
        setCity(_city);
        setFaction(_faction);
        T3ID = _T3ID;
        setPaid(_paid);
        setListGiven(_listGiven);
    }
    
    public Player(String _nickName){
        this(_nickName, "", "", "", "", "", 0, false, false); //NOI18N
    }
    
    //Public Methods
    public static String[] getFactions() {
        return new String[]{ java.util.ResourceBundle.getBundle("txm/resources/languages").getString("Empire"), java.util.ResourceBundle.getBundle("txm/resources/languages").getString("REBELS"), java.util.ResourceBundle.getBundle("txm/resources/languages").getString("SCUM AND VILLAINY") };
    }
        
    public String getDisplayName()    {
        String displayName = getForeName() + " "; //NOI18N
        
        if (!getNickName().isEmpty()){
            displayName += "\"" + getNickName() + "\""; //NOI18N
        } else {
            displayName += getLastName().charAt(0) + "."; //NOI18N
        }
        if(isDisqualified()){
            displayName += " <" + java.util.ResourceBundle.getBundle("txm/resources/languages").getString("DISQUALIFIED") + ">"; 
        } else if (hasDropped()) {
            displayName += " <" + java.util.ResourceBundle.getBundle("txm/resources/languages").getString("DROPPED") + ">";
        }
        return displayName;
    }
    
     public boolean hasPlayedVS(Player enemy) {
        if (getEnemies() == null){
            return false;
        }
        
        for (Player playedEnemy : getEnemies()){
            if (playedEnemy.ID == enemy.ID){
                return true;
            }         
        }
        return false;
    }

    public boolean hasPlayedAndWonVS(Player enemy) {
        if (getEnemies() == null){
            return false;
        }
        
        for (int i = 0;i < getEnemies().size();i++) {
            if(getEnemies().get(i).ID == enemy.ID){
                return (getResults().get(i).getDestroyedPoints() - getResults().get(i).getLostPoints()) > 0;
            }
        }

        return false;
    }
    
    public void sumStrengthOfSchedule(){
        strengthOfSchedule = 0;
        for (int i = 0;i < getEnemies().size();i++)
        {
            strengthOfSchedule += getEnemies().get(i).getTournamentPoints();
        }
    }
    
    public boolean addResult(Result result){     
        return addResult(result, true);
    }
    
    public void removeResult(int round){
        getResults().remove(round - 1);
        removeResult(round, true);
    }

    public void update(Result result, int round){
        removeResult(round, false);
        addResult(result, false);
        getResults().set(round - 1, result);
    }
    
    @Override
    public boolean equals(Object o) {
        if (!(o instanceof Player)) {
            return false;
        }
        Player p = (Player) o;
        return this.ID == p.ID;
    }

    @Override
    public int hashCode() {
        int hash = 7;
        hash = 41 * hash + this.ID;
        return hash;
    }
    
    public void removePlayer(boolean isDisqualified){
        dropped = true;
        if(isDisqualified){
            disqualified = true;
        }       
    }
    
    public void addLastEnemy(Player enemy){
        getEnemies().add(enemy);
        sumStrengthOfSchedule();
    }
     
    //private Methods
    private boolean calculateResult(Result result, BiFunction<Integer, Integer, Integer> f){
        
        //ID == -1 => Bye
        if(result.getEnemy().ID == -1){
            result = new Result((int) 1.5 * result.getMaxPoints(), 0, result.getEnemy(), result.getMaxPoints());
        }
        //ID == -2 => WonBye
        else if(result.getEnemy().ID == -2){
            result = new Result((int) 2 * result.getMaxPoints(), 0, result.getEnemy(), result.getMaxPoints());
        }
        
        int tP = result.getDestroyedPoints() - result.getLostPoints();
        if(tP >= 12){
            tP = 5;
        }
        else if(tP < 12 && tP > 0){
            tP = 3;
        }
        else if(tP == 0){
            tP = 1;
        }
        else {
            tP = 0;
        }
        
        marginOfVictory = f.apply(marginOfVictory, (result.getDestroyedPoints() - result.getLostPoints() + 100));
        destroyedPoints = f.apply(destroyedPoints, result.getDestroyedPoints());
        lostPoints = f.apply(lostPoints, result.getLostPoints());
        tournamentPoints = f.apply(tournamentPoints, tP);
        switch(tP){
            case 5: wins = f.apply(wins, tP);
                    break;
            case 3: modifiedWins = f.apply(modifiedWins, tP);
                    break;
            case 1: draws = f.apply(draws, tP);
                    break;
            case 0: losses = f.apply(losses, tP);
                    break;
        }
        
        return tP >= 3; 
    }    
        
    private void removeResult(int round, boolean removeResult){
        calculateResult(getResults().get(round - 1), (x, y) -> {return x - y;});
    }
       
    private boolean addResult(Result result, boolean addResult){
        getResults().add(result);
        return calculateResult(result, (x, y) -> {return x + y;});
    } 
}
