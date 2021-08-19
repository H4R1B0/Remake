# 모여라 발굴단! 리메이크
###### 과거에 만든게 잦은 버그로 인해 리메이크..^^

## 1. 구성
![image](https://user-images.githubusercontent.com/12294460/129524017-ad965eb6-5db1-499e-bab0-88d79a2b5f50.png)

### (1) 전투 준비
#### 유닛을 구매, 판매 가능. 준비가 완료되어 더 이상 할 것이 없다면 시작 버튼을 눌러 전투 시작.
### (2) 전투
#### 라운드에 따라 배치된 적과 내 유닛들이 자동 전투. 유저는 아무것도 개입할 수 없음.

### (3) 전투 종료
#### 승리 시 - 현재 라운드에 따라 수정과 스타를 정산해 지급. (아군 유닛이 하나라도 살아 있다면 승리로 간주)
#### 패배 시 - 게임 다시 시작하거나 로비로 이동
---
#### 전투가 종료되면 쓰러진 유닛들이 모두 부활, 내가 배치했던 원래 상태로 복귀.
#### 유닛 카드가 자동으로 리프레시.

## 2. 시너지
|종족|필요 인원|효과|
|:---:|:---:|:---:|
|메멀|2, 4|추가체력을 100/200 획득|
|비스트|2, 4|적이 처치되면 최대 체력의 5/10% 회복|
|렙터|2, 4|매초 체력을 5/10, 마나를 1/2 회복|
|인섹트|2, 4|스킬에 필요한 마나 비용이 10/20 감소|
|쉘|2, 4|기본공격피해를 10/20%만큼 덜 입습니다|
|피쉬|2, 4|10/20% 확률로 적의 일반공격을 회피|
|버드|2, 4|크리티컬확률이 10/20% 증가|
|화석|2, 4|공격력이 10/20, 체력이 40/80 증가|

##### 직업 시너지는 아직...

## 3. 유닛 카드 인터페이스
![image](https://user-images.githubusercontent.com/12294460/129524947-d4598762-cba7-4ed8-916c-14db79340148.png)
### (1) 상점 패널
#### 유저가 가지고 있는 유닛 카드 중에 5장이 랜덤으로 등장.
#### 카드를 구매하면 해당카드 비활성화.
#### 카드에 해당되는 유닛이 무작위 칸에 소환.

### (2) 리프레시
#### 수정 10을 사용하여 상점의 모든 목록 초기화.

### (3) 인크리스
#### 유닛 소환 개수 제한을 증가. (최대 7개까지 가능. 즉, 5번 늘릴수있음.)
#### 증가 비용이 점점 증가.

## 4. 인게임에서 사용되는 자원

![image](https://user-images.githubusercontent.com/12294460/129525956-41160c70-2372-4dc9-81b5-8a5870abbfc2.png)
### (1) 수정
#### 매 라운드 종료마다 정해진 수량만큼 지급. 유닛을 소환할 때 소모.

![image](https://user-images.githubusercontent.com/12294460/129525901-44edd90a-3558-44d0-a9a2-70348a68cd85.png)
### (2) 스타포인트
#### 전투가 종료되고 결과에 따라 획득. 충분히 모으면 보상을 획득. 살아남은 유닛의 레벨에 따라 스타포인트 지급.

![image](https://user-images.githubusercontent.com/12294460/129526076-8e99350a-ab47-4287-a3ee-93c38a3cfe0c.png)
### (3) 수용 인구
#### 2명의 인구제한으로 시작. 인크리스 버튼을 눌러 수정을 지불하고 인구제한을 1확장.

## 5. 인게임 배치
![image](https://user-images.githubusercontent.com/12294460/129526157-5ef55764-111c-4022-a3ae-e05053f35450.png)

## 6. 유닛에 대한 정보

### (1) 체력
#### 0이되면 유닛은 사망한 것으로 처리. (비활성화)

### (2) 공격력
#### 기본공격시 수치만큼 적에게 피해.

### (3) 공격속도
#### 유닛의 공격속도. 매우 느림, 느림, 보통, 빠름, 매우 빠름으로 5단계 나눔.

### (4) 사정거리
#### 유닛의 기본공격 사정거리. 1의 사정거리당 유닛을 중심으로 원형태의 범위안에 드는 적을 공격.

### (5) 마나
#### 기본공격을 하거나, 기본공격을 맞으면 5씩 차오름. 100에 도달하면 스킬을 사용.

## 7. 상점 비용
![image](https://user-images.githubusercontent.com/12294460/129526433-902f61aa-da63-4bfb-9b32-3532e4824165.png)
|노말|레어|에픽|전설|
|:---:|:---:|:---:|:---:|
|10|20|30|40|
##### 카드를 구입하면 해당카드와 같은 카드들의 비용이 영구적으로 상승.
##### 유닛을 판매하고 기본비용을 돌려받음. (어차피 플레이어는 10 손해)

![image](https://user-images.githubusercontent.com/12294460/129527579-2c730f1b-539a-4065-86ad-0de0e8ea84e4.png)
|리프레시|인크리스|
|:---:|:---:|
|10|40|
##### 리프레시 10 고정.
##### 인크리스는 사용시 다음 인크리스의 비용이 10 증가.
###### 40 - 80 - 200 - 500 - 1500

## 8. 마나수정 지급
### (1) 수정은 기본적으로 매라운드가 종료될 때마다 승리 시 지급.
#### 라운드x10만큼 수정 지급

### (2) 스타포인트를 채우고 추가 수정을 획득.
#### 스타포인트 10개마다 수정 100(+50) 지급
