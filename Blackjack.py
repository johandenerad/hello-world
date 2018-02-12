
# coding: utf-8

# In[1]:


class Player(object):
    
    def __init__(self,name='Sucker',bankroll=1000,cards=[]):
        self.name = name
        self.bankroll = bankroll
        self.cards = cards  
        
    def update_bankroll(self,value):
        self.bankroll = self.bankroll + value
        
    def add_card(self,card):
        self.cards.append(card)
    
    def clear_cards(self):
        self.cards = []


# In[2]:


class Card(object):
    ranks = ['2','3','4','5','6','7','8','9','10','Jack','Queen','King','Ace']
    suits = ['Hearts','Diamonds','Clubs','Spades']
    
    def __init__(self,suit='',rank='',value=0):
        self.suit = suit
        self.rank = rank
        try:
            self.value = int(self.rank)
        except:
            if self.rank == 'Ace':
                self.value = 11
            else:
                self.value = 10
    
    def __str__(self):
        return "{0} of {1}".format(self.rank,self.suit)
    
    def set_value(self,value):
        self.value = value


# In[3]:


import random

class Deck(object):
    
    def __init__(self,list=[]):
        self.list = list
        
    def build(self):
        self.list = [Card(Card.suits[x],Card.ranks[y]) for x in range(0,4) for y in range(0,13)]
        random.shuffle(self.list)
        return self.list
        
    def draw(self):
        return self.list.pop()
    
    def show_list(self):
        for card in self.list:
            print(str(card))


# In[ ]:


def hand_value(cards):
    return sum(card.value for card in cards)

def game_state(dealer,player,bet=0,dealer_turn=False):
    print('-----------------------------------------------------------------')
    print('{0} (Bank: {1}, Bet: {2})'.format(player.name,player.bankroll,bet))
    print('-----------------------------------------------------------------')
    if dealer_turn == False:
        print('Dealer shows: Hidden, {0}'.format(dealer.cards[1]))
    else:
        print('Dealer shows: {0}. Value: {1}'.format(", ".join(str(card) for card in dealer.cards),hand_value(dealer.cards)))
    print()
    print('Your hand: {0}. Value: {1}'.format(", ".join(str(card) for card in player.cards),hand_value(player.cards)))
    print()


# In[ ]:


print('Welcome to Blackjack! Remember: the house always wins')

player = Player(input('Gotta name? '))
dealer = Player('Dealer',0)

replay = True

while replay == True:
    player.clear_cards()
    dealer.clear_cards()

    deck = Deck()
    deck.build()
    
    bet = 0
    while True:
        try:
            bet = int(input('Place a bet(10-250): '))
        except:
            print('Enter a numerical value between 10 and 250')
            continue
        if bet < 10 or bet > 250:
            print('Bet must be between 10 and 250')
            continue
        else:
            print('Bet is: {0}'.format(bet))
            break
    player.update_bankroll(-bet)
            
    player.add_card(deck.draw())
    dealer.add_card(deck.draw())
    player.add_card(deck.draw())
    dealer.add_card(deck.draw())

    game_state(dealer,player)
    player_bust = False
    player_blackjack = False
    
    if hand_value(player.cards) == 21:
        print(player.name + ' has Blackjack!')
        player_blackjack = True
        
    #player turn
    while not player_blackjack:
        go = (input('[D]raw or [S]tick? ')).lower()
        if go == 'draw' or go == 'd':
            player.add_card(deck.draw())
            game_state(dealer,player,bet)
            if hand_value(player.cards) > 21:
                if any(card.rank == 'Ace' for card in player.cards):
                    for card in player.cards:
                        if card.rank == 'Ace':
                            card.set_value(1)
                    print('Aces reduced to 1.')
                    print('New hand value: {0}'.format(hand_value(player.cards)))
                if hand_value(player.cards) > 21:
                    print('You are bust!')
                    player_bust = True
                    print('Bank: {0}'.format(player.bankroll))
                    break
            continue
        if go == 'stick' or go == 's':
            print('Stick on: {0}. Dealer\'s turn.'.format(hand_value(player.cards)))
            break

    #dealer turn        
    while not player_bust:
        if player_blackjack:
            if hand_value(dealer.cards) == 21:
                game_state(dealer,player,bet,True)
                print('Dealer has Blackjack!')
                print('A draw.')
                print('{0} wins {1}'.format(player.name,bet))
                player.update_bankroll(bet)
                break
            else:
                game_state(dealer,player,bet,True)
                print('{0} wins {1}'.format(player.name,2*bet))
                player.update_bankroll(2*bet)
                break
            
        if hand_value(dealer.cards) < 17 or hand_value(dealer.cards) < hand_value(player.cards):
            print('Dealer draws...')
            dealer.add_card(deck.draw())

        if hand_value(dealer.cards) > 21:
            if any(card.rank == 'Ace' for card in dealer.cards):
                    for card in dealer.cards:
                        if card.rank == 'Ace':
                            card.set_value(1)
                    print('Aces reduced to 1.')
            if hand_value(dealer.cards) > 21:
                game_state(dealer,player,bet,True)
                print('Dealer bust!')
                player.update_bankroll(2*bet)
                print('{0} wins {1}'.format(player.name,2*bet))
                print('Bank: {0}'.format(player.bankroll))
                break

        if hand_value(dealer.cards) == hand_value(player.cards):
            game_state(dealer,player,bet,True)
            print('A draw.')
            player.update_bankroll(bet)
            print('{0} wins {1}'.format(player.name,bet))
            print('Bank: {0}'.format(player.bankroll))
            break

        if hand_value(dealer.cards) > hand_value(player.cards):
            game_state(dealer,player,bet,True)
            print('Dealer wins, {0} to {1}.'.format(hand_value(dealer.cards),hand_value(player.cards)))
            print('Bank: {0}'.format(player.bankroll))
            break

    while True:
        try:
            again = input('Another hand? [Y]es or [N]o').lower()
        except:
            print('Please enter: [Y]es or [N]o')
            continue
        if again == 'yes' or again == 'y':
            replay = True
            break
        elif again == 'no' or again == 'n':
            print('Thankyou for playing!')
            replay = False
            break
        else:
            print('Please enter: [Y]es or [N]o')
            continue

